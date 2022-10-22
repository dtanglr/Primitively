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
        internal static readonly string DateOnlyAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.DateOnlyAttribute));
        internal static readonly string GuidPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.GuidPrimitiveAttribute));
        internal static readonly string IPrimitive = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.IPrimitive));
        internal static readonly string NhsNumberPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.NhsNumberPrimitiveAttribute));
        internal static readonly string OdsCodePrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.OdsCodePrimitiveAttribute));
        internal static readonly string PostcodePrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.PostcodePrimitiveAttribute));
        internal static readonly string StringPrimitiveAttribute = GetEmbeddedResource(nameof(Abstractions), nameof(Primitively.StringPrimitiveAttribute));

        internal static Dictionary<string, string> GetEmbeddedResources() => new()
        {
            { nameof(Constants), Constants },
            { nameof(DateOnlyAttribute), DateOnlyAttribute },
            { nameof(GuidPrimitiveAttribute), GuidPrimitiveAttribute },
            { nameof(IPrimitive), IPrimitive },
            { nameof(NhsNumberPrimitiveAttribute), NhsNumberPrimitiveAttribute },
            { nameof(OdsCodePrimitiveAttribute), OdsCodePrimitiveAttribute },
            { nameof(PostcodePrimitiveAttribute), PostcodePrimitiveAttribute },
            { nameof(StringPrimitiveAttribute), StringPrimitiveAttribute }
        };
    }

    internal static class DateOnly
    {
        internal static readonly string Base = GetEmbeddedResource(nameof(DateOnly), nameof(Base));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(DateOnly), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(DateOnly), nameof(TypeConverter));
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
        internal static readonly string DefaultPartialMethods = GetEmbeddedResource(nameof(String), nameof(DefaultPartialMethods));
        internal static readonly string JsonConverter = GetEmbeddedResource(nameof(String), nameof(JsonConverter));
        internal static readonly string TypeConverter = GetEmbeddedResource(nameof(String), nameof(TypeConverter));

        // Customisations: replacing DefaultPartialMethods
        internal static readonly string NhsNumberMethods = GetEmbeddedResource(nameof(String), nameof(NhsNumberMethods));
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
