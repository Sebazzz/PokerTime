# ![Icon](doc/logo.png) PokerTime

Remote planning poker tool built in ASP.NET Core and Blazor

Licensed: GNU GPL v3.0

|                    | master                                                                                                                                                                                                                  | develop                                                                                                                                                                                                                   |
| ------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **AppVeyor CI**    | [![Build status](https://ci.appveyor.com/api/projects/status/mlwomeg23hqb0r3j/branch/master?svg=true)](https://ci.appveyor.com/project/Sebazzz/PokerTime/branch/master)                                                 | [![Build status](https://ci.appveyor.com/api/projects/status/mlwomeg23hqb0r3j/branch/develop?svg=true)](https://ci.appveyor.com/project/Sebazzz/PokerTime/branch/develop)                                                 |
| **CircleCI**       | [![CircleCI](https://circleci.com/gh/Sebazzz/PokerTime/tree/master.svg?style=shield)](https://circleci.com/gh/Sebazzz/PokerTime/tree/master)                                                                            | [![CircleCI](https://circleci.com/gh/Sebazzz/PokerTime/tree/develop.svg?style=shield)](https://circleci.com/gh/Sebazzz/PokerTime/tree/develop)                                                                            |
| **Github actions** | [![Github CI](https://github.com/sebazzz/PokerTime/workflows/Continuous%20integration/badge.svg?branch=master)](https://github.com/Sebazzz/PokerTime/actions?workflow=Continuous+integration&branch=master)             | [![Github CI](https://github.com/sebazzz/PokerTime/workflows/Continuous%20integration/badge.svg?branch=develop)](https://github.com/Sebazzz/PokerTime/actions?workflow=Continuous+integration&branch=develop)             |
| **Codeconv**       | [![codecov](https://codecov.io/gh/Sebazzz/PokerTime/branch/master/graph/badge.svg)](https://codecov.io/gh/Sebazzz/PokerTime)                                                                                            | [![codecov](https://codecov.io/gh/Sebazzz/PokerTime/branch/develop/graph/badge.svg)](https://codecov.io/gh/Sebazzz/PokerTime)                                                                                             |
| **Daily build**    | [![Github CI](https://github.com/sebazzz/PokerTime/workflows/Build%20installation%20packages/badge.svg?branch=master)](https://github.com/Sebazzz/PokerTime/actions?workflow=Build+installation+packages&branch=master) | [![Github CI](https://github.com/sebazzz/PokerTime/workflows/Build%20installation%20packages/badge.svg?branch=develop)](https://github.com/Sebazzz/PokerTime/actions?workflow=Build+installation+packages&branch=develop) |

## Features

-   Realtime planning poker app, ideal for remote teams
-   Create password protected planning poker sessions
-   As facilitator, lead the planning poker session through the discussion, estimation and consolidation stages
-   Easy to use

### Browser Support

Developed and tested on:

-   Internet Explorer 11
-   Microsoft Edge
-   Google Chrome
-   Mozilla Firefox

## Download

### Docker

PokerTime is available as a docker image. Simply pull it from the Docker hub, and run it:

    docker pull sebazzz/pokertime:latest
    docker run -p 80:80 sebazzz/pokertime

For further configuration you may want to mount a directory with [the configuration](doc/Installation.md#Configuration):

    docker run -p 80:80 -v /path/to/my/configuration/directory:/etc/pokertime sebazzz/pokertime

### Manual installation

Download the release for your OS from the [releases tab](https://github.com/Sebazzz/PokerTime/releases) or download the cutting edge builds from [AppVeyor](https://ci.appveyor.com/project/Sebazzz/PokerTime).

[Follow the installation instructions](doc/Installation.md) in the documentation to install it.

## Building PokerTime from sources

If you prefer to build the application yourself, please follow the [compilation instructions](doc/Building-from-sources.md) in the documentation.

## Screenshots

**Create a planning poker session**

![Create a planning poker session](doc/create-session.png)

**Join a planning poker session**

![Join a planning poker session](doc/join-poker-session.png)

**Discussion stage**

![Discussion stage](doc/discussion.png)

**Estimation**
![Estimation](doc/estimation.png)

**Coming to a consensus**
![Estimation discussion](doc/estimation-discussion.png)

**Session conclusion**
![Conclusion](doc/finished.png)

## Contributions

Contributions are allowed and encouraged. In general the rules are: same code style (simply use the included `.editorconfig`), and write automated tests for the changes.

Please submit an issue to communicate in advance to prevent disappointments.

## Attribution

Application icon:

-   Icon made by [Eucalyp](https://www.flaticon.com/authors/eucalyp) from [www.flaticon.com](http://www.flaticon.com/)

Built on:

-   [Bulma](https://bulma.io) _CSS framework_;
-   [Fontawesome](http://fontawesome.io/) as _icon framework_;
-   [ASP.NET Core 3.1](https://dot.net) (Blazor Server) with [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for _server side logic and data persistence_;
