# Test1 Calculator

This project is a Windows calculator application. It uses C# on .NET 8 and WinForms. The calculation logic lives in a separate engine library. The engine has no dependency on Windows or WinForms. You can test the engine on any operating system.

## Build

Run this command on Windows:

```text
dotnet build test1.sln -c Release
```

## Test

On Windows, run the full solution tests:

```text
dotnet test test1.sln -c Release
```

On any operating system that has Docker, run the engine tests in a container. TRX files are written to `docker/TestResults/`:

```text
docker compose -f docker/docker-compose.test.yml run --rm tests
```