---
_layout: landing
---

![Primitively](images/logo-143x153.png)

# Primitively

Primitively is a powerful C# source generator that transforms primitive identifiers and value objects into highly performant, customisable, read-only struct values that support ASP.NET model binding and validation (including FluentValidation), Open API standards, JSON and MongoDB BSON serialization, with zero or minimal configuration.

The 1.4.x release supports the following platforms:

- .NET 8
- .NET 7
- .NET 6
- .NET 5¹
- .NET Core 3.1¹
- .NET Standard 2.0¹

¹ It is possible (but not recommended) to use Primitively on class libraries that target platforms that preceed .NET 6. C# 10 is a minimum requirement so make sure your project file's `LangVersion` is set to `latest` or `10 or higher` (see below). To learn more, clone the Primitively git repo and open the example projects.

```vs
<PropertyGroup>
   <LangVersion>latest</LangVersion>
</PropertyGroup>
```