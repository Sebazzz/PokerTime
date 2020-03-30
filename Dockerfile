### BUILD
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201 AS build-env
WORKDIR /source

# Prerequisites
COPY utils/* utils/
RUN utils/install-all-prereqs.sh

# ... libgit2sharp debian 10.x support
RUN dotnet tool install -g GitVersion.Tool --version 5.2.4
ENV LD_LIBRARY_PATH=/root/.dotnet/tools/.store/gitversion.tool/5.2.4/gitversion.tool/5.2.4/tools/netcoreapp3.1/any/runtimes/debian.9-x64/native/

# Copy csproj and restore as distinct layers
# ... sources
COPY src/PokerTime.Application/*.csproj src/PokerTime.Application/
COPY src/PokerTime.Common/*.csproj src/PokerTime.Common/
COPY src/PokerTime.Domain/*.csproj src/PokerTime.Domain/
COPY src/PokerTime.Infrastructure/*.csproj src/PokerTime.Infrastructure/
COPY src/PokerTime.Persistence/*.csproj src/PokerTime.Persistence/
COPY src/PokerTime.Web/*.csproj src/PokerTime.Web/
COPY src/Common.props src/

# ... tests
COPY tests/PokerTime.Application.Tests.Unit/*.csproj tests/PokerTime.Application.Tests.Unit/
COPY tests/PokerTime.Domain.Tests.Unit/*.csproj tests/PokerTime.Domain.Tests.Unit/
COPY tests/PokerTime.Web.Tests.Unit/*.csproj tests/PokerTime.Web.Tests.Unit/
COPY tests/PokerTime.Web.Tests.Integration/*.csproj tests/PokerTime.Web.Tests.Integration/
COPY tests/Common.props tests/

COPY *.sln .
COPY dotnet-tools.json .
RUN dotnet restore
RUN dotnet tool restore

# Yarn (although it isn't as large, still worth caching)
COPY src/PokerTime.Web/package.json src/PokerTime.Web/
COPY src/PokerTime.Web/yarn.lock src/PokerTime.Web/
RUN yarn --cwd src/PokerTime.Web/

## Skip build script pre-warm
## This causes later invocations of the build script to fail with "Failed to uninstall tool package 'cake.tool': Invalid cross-device link"
#COPY build.* .
#RUN ./build.sh --target=restore-node-packages

### TEST
FROM build-env AS test

# ... run tests
COPY . .
RUN ./build.sh --target=test

### PUBLISHING
FROM build-env AS publish

# ... run publish
COPY . .
RUN ./build.sh --target=Publish-Ubuntu-18.04-x64 --publish-dir=publish --verbosity=verbose --skip-compression=true

### RUNTIME IMAGE
FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1
WORKDIR /app

# ... Run libgdi install
COPY utils/install-app-prereqs.sh utils/
RUN bash utils/install-app-prereqs.sh

# ... Copy published app
COPY --from=publish /source/publish/ubuntu.18.04-x64/ .

ENV ASPNETCORE_ENVIRONMENT Production

# Config directory
VOLUME ["/etc/pokertime"]

# Set some defaults for a "direct run" experience
ENV DATABASE__DATABASE "/app/data.db"
ENV DATABASE__DATABASEPROVIDER Sqlite

# ... enable proxy mode
ENV SECURITY__ENABLEPROXYMODE True

# ... health check
HEALTHCHECK CMD curl --fail http://localhost/health || exit

ENTRYPOINT [ "./launch", "run" ]