FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.1
WORKDIR /app

# ... Run libgdi install
COPY utils/install-app-prereqs.sh utils/
RUN bash utils/install-app-prereqs.sh

# ... Copy published app
COPY build/publish/ubuntu.18.04-x64/ ./

ENV ASPNETCORE_ENVIRONMENT Production
ENV DATABASE__DATABASE "/app/data.db"
ENV DATABASE__DATABASEPROVIDER Sqlite
ENTRYPOINT [ "./launch", "run" ]