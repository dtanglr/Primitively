---
_layout: landing
---

![Primitively](images/logo-143x153.png)

# Primitively

Primitively is a powerful C# source generator that transforms primitive identifiers and value objects into highly performant, customisable, read-only struct values that support ASP.NET model binding and validation (including FluentValidation), Open API standards, JSON and MongoDB BSON serialization, with zero or minimal configuration.

The forthcoming 1.4 release supports the following platforms:

- .NET 8
- .NET 7
- .NET 6
- .NET 5¹
- .NET Core 3.1¹
- .NET Standard 2.0¹

Prior to the 1.4 release. Primitively supported .NET 6 only.

¹ It is possible (but not recommended) to use Primitively on class libraries that target platforms that preceed .NET 6. Extra configuration is required for this to work because C# 10 is a minimum requirement. Please see the documentation for details. 