# Primitively

[![CI](https://github.com/dtanglr/Primitively/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/dtanglr/Primitively/actions/workflows/dotnet.yml)
[![NuGet version](https://badge.fury.io/nu/Primitively.svg)](https://badge.fury.io/nu/Primitively)

**Primitively** is a Rosyln-powered C# source code generator of strongly-typed IDs and DDD-style value objects that encapsulate a single GUID, integer, date or string .NET **primitively** typed value.

```csharp
// Before
public record Product(Guid Id, Guid Sku);
```

```csharp
// After
public record Product(ProductId Id, Sku Sku);
```

```csharp
[Guid]
public partial record struct ProductId;

[Guid]
public partial record struct Sku;
```

