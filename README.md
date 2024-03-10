![Primitively](docs/images/logo-143x153.png)

# Primitively  [![CI](https://github.com/dtanglr/Primitively/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/dtanglr/Primitively/actions/workflows/dotnet.yml)

**Primitively** is a Rosyln-powered C# source code generator of strongly-typed IDs and DDD-style value objects that encapsulate a single GUID, integer, date or string .NET *primitively* typed value.

For example: -

```csharp
// Before
public record Product(Guid Id, Guid Sku);

// After
public record Product(ProductId Id, Sku Sku);

[Guid]
public partial record struct ProductId;

[Guid]
public partial record struct Sku;
```

## NuGet Packages

| **Package** | **Latest Version** | **About** |
|:--|:--|:--|
| `Primitively` | [![NuGet](https://buildstats.info/nuget/Primitively?includePreReleases=true)](https://www.nuget.org/packages/Primitively/ "Download Primitively from NuGet.org") | The Primitively source generator package. |
| `Primitively.Abstractions` | [![NuGet](https://buildstats.info/nuget/Primitively.Abstractions?includePreReleases=true)](https://www.nuget.org/packages/Primitively.Abstractions/ "Download Primitively.Abstractions from NuGet.org") | Primitively interfaces, metadata, attributes and configuration classes. |
| `Primitively.AspNetCore.Mvc` | [![NuGet](https://buildstats.info/nuget/Primitively.AspNetCore.Mvc?includePreReleases=true)](https://www.nuget.org/packages/Primitively.AspNetCore.Mvc/ "Download Primitively.AspNetCore.Mvc from NuGet.org") | ASP.NET MVC model binding support for Primitively types used in route and querystring parameters. |
| `Primitively.AspNetCore.SwaggerGen` | [![NuGet](https://buildstats.info/nuget/Primitively.AspNetCore.SwaggerGen?includePreReleases=true)](https://www.nuget.org/packages/Primitively.AspNetCore.SwaggerGen/ "Download Primitively.AspNetCore.SwaggerGen from NuGet.org") | Swagger Open API schema support for Primitively types using Swashbuckle. |
| `Primitively.MongoDB.Bson` | [![NuGet](https://buildstats.info/nuget/Primitively.MongoDB.Bson?includePreReleases=true)](https://www.nuget.org/packages/Primitively.MongoDB.Bson/ "Download Primitively.MongoDB.Bson from NuGet.org") | BSON serialization for Primitively types stored in MongoDB. |

## Documentation

This README is designed get you started generating your own strongly-typed identifiers with just a few lines of code.

For more detailed information about Primitively, check out [primitively.net](https://primitively.net).

## Quick start

To get started, first add the [Primitively](https://www.nuget.org/packages/Primitively/) NuGet package to your project by running the following command:

```sh
dotnet add package Primitively --prerelease
```

Open your csproj file and edit the package reference, setting `PrivateAssets="All"`. The file will look something like this afterwards:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Primitively" Version="1.4.15-rc.1" PrivateAssets="All" />
  </ItemGroup>
  
</Project>
```

You are now ready to create your first Primitively source generated type!

Create a new class file and add a reference to Primitively and decorate your `partial record struct` with one of the Primitively attributes such as `[Guid]`.

For example: -

```cs
using Primitively;

namespace Acme.Examples;

[Guid]
public partial record struct ProductId;
```

Here's a list of all the Primitively attributes currently available: -

- Date and time
  - `[DateOnly]`
- Globally unique identifiers
  - `[Guid]`
- Integers
  - `[Byte]`
  - `[Int]`
  - `[Long]`
  - `[SByte]`
  - `[Short]`
  - `[UInt]`
  - `[ULong]`
  - `[UShort]`
- Strings
  - `[String]`

Here's some source generation in action using each of the above attributes: -

![Primitively examples](docs/images/source-gen-anim-01.gif)

## Using Primitively on an ASP.NET core web project

If you want to use your Primitively types in an ASP.NET core web project. Add the [Primitively.AspNetCore.Mvc](https://www.nuget.org/packages/Primitively.AspNetCore.Mvc/) NuGet package to your project by running the following command. This package contains model binding support, which means you can use your Primitively types in request headers and query strings.

```sh
dotnet add package Primitively.AspNetCore.Mvc --prerelease
```

If you are also generating **swagger** documentation for a web API. Add the [Primitively.AspNetCore.SwaggerGen](https://www.nuget.org/packages/Primitively.AspNetCore.SwaggerGen/) NuGet package to your project by running the following command. 

```sh
dotnet add package Primitively.AspNetCore.SwaggerGen --prerelease
```

### Dependency injection

Here's an example of the DI setup for a C# ASP.NET web api project that references a class library containing Primitively source generated types and is generating **swagger** documentation.

```cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add primitively configuration
builder.Services.AddPrimitively(options =>
{
    // Register the location of source generated Primitively types within the application
    // NB. No need to use reflection to scan assemblies! Each class library that contains source
    // generated Primitively types also has a 'PrimitiveLibrary' static helper class. Meta data such
    // as type name, underlying data type, example value, min / max Length etc can then be obtained
    // from the PrimitiveRepository instance within each class library
    options.Register(Acme.Lib1.PrimitiveLibrary.Respository);
})
// Add AspNetCore Mvc model binding support for Primitively types used in APIs.  This means strongly typed
// Primitively types can easily be used as querystring and route params too etc
.AddMvc()
// Add Swashbuckle Open Api Schema Filter so Primitively types are fully supported in the API Swagger documentation 
.AddSwaggerGen();
```
