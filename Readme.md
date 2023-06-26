# FSharp.Data.Adaptive Demos

This repo contains the Examples shown in the F#-Conf Presentation about [FSharp.Data.Adaptive](https://github.com/fsprojects/FSharp.Data.Adaptive) held on 2023/06/26.

## AdaptiveDemo

Basic examples using simple FSharp.Data.Adaptive datastructures

## UI

Demonstrates usage of the library in the context of UI / Rendering by using [Aardvark.Dom](https://github.com/aardvark-community/aardvark.dom) for the UI definition and the Browser integration from [Aardworx.WebAssembly](https://github.com/aardworx/aardworx.webassembly)

## Building

1. `dotnet tool restore`
2. `dotnet paket restore`
3. `dotnet run` in one of the project-folders

In order to run the UI example you need to install `wasm-tools` which can be done via `dotnet workload install wasm-tools`
