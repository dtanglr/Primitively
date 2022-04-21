﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Primitively;

internal static class EmbeddedResources
{
    private static readonly Assembly _thisAssembly = typeof(EmbeddedResources).Assembly;

    internal static readonly string AutoGeneratedHeader = GetEmbeddedResource(nameof(AutoGeneratedHeader));
    internal static readonly string JsonConverterAttribute = GetEmbeddedResource(nameof(JsonConverterAttribute));
    internal static readonly string TypeConverterAttribute = GetEmbeddedResource(nameof(TypeConverterAttribute));

    internal static class Abstractions
    {
        internal static readonly string Constants = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.Constants));
        internal static readonly string DatePrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.DatePrimitiveAttribute));
        internal static readonly string GuidPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.GuidPrimitiveAttribute));
        internal static readonly string IPrimitive = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IPrimitive));
        internal static readonly string IPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IPrimitiveAttribute));
        internal static readonly string IStringLength = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IStringLength));
        internal static readonly string StringLength = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.StringLength));
        internal static readonly string StringLengthRange = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.StringLengthRange));
        internal static readonly string StringPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.StringPrimitiveAttribute));

        internal static Dictionary<string, string> GetEmbeddedResources() => new()
        {
            { nameof(Constants), Constants },
            { nameof(DatePrimitiveAttribute), DatePrimitiveAttribute },
            { nameof(GuidPrimitiveAttribute), GuidPrimitiveAttribute },
            { nameof(IPrimitive), IPrimitive },
            { nameof(IPrimitiveAttribute), IPrimitiveAttribute },
            { nameof(IStringLength), IStringLength },
            { nameof(StringLength), StringLength },
            { nameof(StringLengthRange), StringLengthRange },
            { nameof(StringPrimitiveAttribute), StringPrimitiveAttribute }
        };
    }

    internal static class Date
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(Date), nameof(Base));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(Date), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(Date), nameof(TypeConverter));
    }

    internal static class Guid
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(Guid), nameof(Base));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(Guid), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(Guid), nameof(TypeConverter));
    }

    internal static class String
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(String), nameof(Base));
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

    private static string MakeInternal(this string resource)
        => resource.Replace("public sealed", "internal sealed")
                   .Replace("public enum", "internal enum")
                   .Replace("public interface", "internal interface");
}
