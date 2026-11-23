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

## Publish and run

To build a single self-contained executable for Windows x64, run:

```text
dotnet publish src/Test1.Calculator.App -c Release -p:PublishProfile=win-x64
```

The output file is:

```text
src/Test1.Calculator.App/bin/Release/net8.0-windows/win-x64/publish/Test1.Calculator.App.exe
```

You do not need to install .NET on the target computer. The executable includes the .NET runtime.

Supported systems are Windows 10 and Windows 11 on x64.

To start the application, copy the single `.exe` file to the target computer and double-click it.

The executable size is approximately 150–200 MB.

The executable is not code-signed. If you download it from the internet, Windows SmartScreen can show a security warning. You can choose "More info" and then "Run anyway" to start the application.