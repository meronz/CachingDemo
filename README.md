# Caching demo

This project shows the usage of [FusionCache](https://github.com/ZiggyCreatures/FusionCache) to speed up 
the retrieval of data from a Postgres database.

- ASP.NET Core 6.0
- Entity Framework Core 6.0
- FusionCache

## Quickstart

1. First install Postgres and Redis.
    ```sh
    docker-compose up
    ```

2. Run the demo
    ```sh
    cd CachingDemo
    dotnet run
    ```

## How it works
This project exposes a simple API to create and fetch `User` objects. Data is persisted in Postgres, but to reduce
latency when serving these requests, it is also cached in two layers: First in-memory, and second on Redis.
Redis actually serves two purposes here, both as a cache and as synchronization Backplane. More info about that
on the FusionCache docs.