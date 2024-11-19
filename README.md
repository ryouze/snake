# snake

[![CI](https://github.com/ryouze/snake/actions/workflows/ci.yml/badge.svg)](https://github.com/ryouze/snake/actions/workflows/ci.yml)
[![Release](https://github.com/ryouze/snake/actions/workflows/release.yml/badge.svg)](https://github.com/ryouze/snake/actions/workflows/release.yml)
![Release version](https://img.shields.io/github/v/release/ryouze/snake)

snake is a cross-platform TUI snake game.


## Motivation

I wanted to create a simple game to learn more about TUI (text-based user interface) programming.


## Features

- Written in modern C# (.NET 8).
- Comprehensive code documentation.


## Tested Systems

This project has been tested on the following systems:

- macOS 15.1 (Sequoia)
<!-- - Manjaro 24.0 (Wynsdey)
- Windows 11 23H2 -->

Automated testing is also performed on the latest versions of macOS, GNU/Linux, and Windows using GitHub Actions.


## Pre-built Binaries

Pre-built [framework-dependent executables](https://learn.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli) are available for macOS (ARM64), GNU/Linux (x86_64), and Windows (x86_64). You can download the latest version from the [Releases](../../releases) page.

**Note:** These executables require .NET 8.0 runtime (or later). You can download the runtime from the [.NET website](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime) (`brew install --cask dotnet` on macOS). Alternatively, you can download the [full SDK](https://dotnet.microsoft.com/en-us/download) (`brew install --cask dotnet-sdk` on macOS) to build the project from source.

To remove macOS quarantine, use the following commands:

```sh
xattr -d com.apple.quarantine snake-macos-arm64
chmod +x snake-macos-arm64
```

On Windows, the OS might complain about the executable being unsigned. You can bypass this by clicking on "More info" and then "Run anyway".


## Requirements

To build and run this project, you'll need:

- .NET SDK 8.0 or later (for pre-built executables, only the runtime is required)


## Build

Follow these steps to build the project:

1. **Clone the repository**:

    ```sh
    git clone https://github.com/ryouze/snake.git
    ```

2. **Compile the project**:

    ```sh
    cd snake
    dotnet publish
    ```

After successful framework-dependent executable compilation, you can run the program using `./publish/snake`. However, it is highly recommended to install the program, so that it can be run from any directory. Refer to the [Install](#install) section below.

**Note:** The mode is set to `Release` by default. To build in `Debug` mode, use ``dotnet publish --configuration Debug`.


## Install

If not already built, follow the steps in the [Build](#build) section.

To install the program, use the following command:

```sh
sudo cp publish/snake /usr/local/bin
```


## Usage

To run the program, use the following command:

```sh
snake
```


## Development and Testing

To build and run the program from source, use:

```sh
dotnet run --project Snake/Snake.csproj
```

**Note:** By default, the program will be built and ran in `Debug` mode. To build in `Release` mode, use `dotnet run --project Snake/Snake.csproj --configuration Release`.

To create a self-contained executable, use:

```sh
dotnet publish --self-contained
```

**Note:** The `/p:PublishTrimmed=true` flag causes issues with the `Spectre.Console` library. As a result, the untrimmed executable is around 75 MB.

To run the tests manually, use:

```sh
dotnet test
```


## Credits

- [Spectre.Console](https://spectreconsole.net/)
- [xUnit.net](https://xunit.net/)


## Contributing

All contributions are welcome.


## License

This project is licensed under the MIT License.
