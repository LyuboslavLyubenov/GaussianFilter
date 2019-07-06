# Gaussian blur library/utility

Small application for bluring images with gaussian blur. Its possible to use part of it for developing another filters

## Requirements
- .netcore >= 2.2 
- MatrixEssentials

## Building
First restore packages:
```
dotnet restore
```
then build:
```
dotnet build
```

## Usage
```
dotnet run -- [param] [param2] [param3] ...
```

### Parameters

First is input image path
Second is output image path
Third is gaussian kernel size
Fourth is standard deviation 

Third and fourth parameters are optional

### Examples: 
```
dotnet run -- "./image.png" "./blureed-image.png"
```
```
dotnet run -- "./image.png" "./blurred-image.png" 7 1.0
```