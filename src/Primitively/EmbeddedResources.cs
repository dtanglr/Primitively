﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Primitively;

internal struct EmbeddedResources
{
    private static readonly Assembly _thisAssembly = typeof(EmbeddedResources).Assembly;

    internal static readonly string AutoGeneratedHeader = GetEmbeddedResource(nameof(AutoGeneratedHeader));
    internal static readonly string JsonConverterAttribute = GetEmbeddedResource(nameof(JsonConverterAttribute));
    internal static readonly string TypeConverterAttribute = GetEmbeddedResource(nameof(TypeConverterAttribute));
    internal static readonly string ValidateMethod = GetEmbeddedResource(nameof(ValidateMethod));

    internal struct Abstractions
    {
        internal static readonly string Constants = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.Constants));
        internal static readonly string DateOnlyAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.DateOnlyAttribute));
        internal static readonly string GuidAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.GuidAttribute));
        internal static readonly string IDateOnly = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IDateOnly));
        internal static readonly string IGuid = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IGuid));
        internal static readonly string IPrimitive = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IPrimitive));
        internal static readonly string IString = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IString));
        internal static readonly string Specifier = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.Specifier));
        internal static readonly string StringAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.StringAttribute));

        internal static Dictionary<string, string> GetEmbeddedResources() => new()
        {
            { nameof(Constants), Constants },
            { nameof(DateOnlyAttribute), DateOnlyAttribute },
            { nameof(GuidAttribute), GuidAttribute },
            { nameof(IDateOnly), IDateOnly },
            { nameof(IGuid), IGuid },
            { nameof(IPrimitive), IPrimitive },
            { nameof(IString), IString },
            { nameof(Specifier), Specifier },
            { nameof(StringAttribute), StringAttribute }
        };
    }

    internal struct DateOnly
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(DateOnly), nameof(Base));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(DateOnly), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(DateOnly), nameof(TypeConverter));
    }

    internal struct Guid
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(Guid), nameof(Base));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(Guid), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(Guid), nameof(TypeConverter));
    }

    internal struct String
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(String), nameof(Base));
        internal static readonly string DefaultPartialMethods = GetEmbeddedResource(nameof(String), nameof(DefaultPartialMethods));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(String), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(String), nameof(TypeConverter));
    }

    private static string GetEmbeddedResource(params string[] names)
    {
        var resourceFullName = $"{nameof(Primitively)}.{nameof(EmbeddedResources)}.{string.Join(".", names)}.cs";
        var resourceStream = _thisAssembly.GetManifestResourceStream(resourceFullName);

        if (resourceStream is null)
        {
            var existingResources = _thisAssembly.GetManifestResourceNames();

            throw new ArgumentException($"Could not find embedded resource '{resourceFullName}'. Available names: {string.Join(", ", existingResources)}");
        }

        using var reader = new StreamReader(resourceStream, Encoding.UTF8);

        return reader.ReadToEnd();
    }
}
