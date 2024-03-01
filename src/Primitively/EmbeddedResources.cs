﻿using System;
using System.IO;
using System.Text;

namespace Primitively;

/// <summary>
/// Contains methods for retrieving embedded resources.
/// </summary>
internal readonly struct EmbeddedResources
{
    public static readonly string AutoGeneratedHeader = GetEmbeddedResource(nameof(AutoGeneratedHeader));
    public static readonly string JsonConverterAttribute = GetEmbeddedResource(nameof(JsonConverterAttribute));
    public static readonly string PrimitiveFactory = GetEmbeddedResource(nameof(PrimitiveFactory));
    public static readonly string PrimitiveLibrary = GetEmbeddedResource(nameof(PrimitiveLibrary));
    public static readonly string PrimitiveRepository = GetEmbeddedResource(nameof(PrimitiveRepository));
    public static readonly string TypeConverterAttribute = GetEmbeddedResource(nameof(TypeConverterAttribute));
    public static readonly string ValidateMethod = GetEmbeddedResource(nameof(ValidateMethod));

    /// <summary>
    /// Contains methods for retrieving DateOnly related embedded resources.
    /// </summary>
    public readonly struct DateOnly
    {
        public static readonly string Base = GetEmbeddedResource(nameof(DateOnly), nameof(Base));
        public static readonly string JsonConverter = GetEmbeddedResource(nameof(DateOnly), nameof(JsonConverter));
        public static readonly string TypeConverter = GetEmbeddedResource(nameof(DateOnly), nameof(TypeConverter));
    }

    /// <summary>
    /// Contains methods for retrieving Guid related embedded resources.
    /// </summary>
    public readonly struct Guid
    {
        public static readonly string Base = GetEmbeddedResource(nameof(Guid), nameof(Base));
        public static readonly string JsonConverter = GetEmbeddedResource(nameof(Guid), nameof(JsonConverter));
        public static readonly string TypeConverter = GetEmbeddedResource(nameof(Guid), nameof(TypeConverter));
    }

    /// <summary>
    /// Contains methods for retrieving Integer related embedded resources.
    /// </summary>
    public readonly struct Integer
    {
        public static readonly string Base = GetEmbeddedResource(nameof(Integer), nameof(Base));
        public static readonly string JsonConverter = GetEmbeddedResource(nameof(Integer), nameof(JsonConverter));
        public static readonly string TypeConverter = GetEmbeddedResource(nameof(Integer), nameof(TypeConverter));
    }

    /// <summary>
    /// Contains methods for retrieving String related embedded resources.
    /// </summary>
    public readonly struct String
    {
        public static readonly string Base = GetEmbeddedResource(nameof(String), nameof(Base));
        public static readonly string DefaultPartialMethods = GetEmbeddedResource(nameof(String), nameof(DefaultPartialMethods));
        public static readonly string JsonConverter = GetEmbeddedResource(nameof(String), nameof(JsonConverter));
        public static readonly string TypeConverter = GetEmbeddedResource(nameof(String), nameof(TypeConverter));
    }

    /// <summary>
    /// Retrieves the specified embedded resource.
    /// </summary>
    /// <param name="names">The names of the embedded resources.</param>
    /// <returns>The contents of the embedded resource.</returns>
    private static string GetEmbeddedResource(params string[] names)
    {
        var thisAssembly = typeof(EmbeddedResources).Assembly;
        var resourceFullName = $"{nameof(Primitively)}.{nameof(EmbeddedResources)}.{string.Join(".", names)}.cs";
        var resourceStream = thisAssembly.GetManifestResourceStream(resourceFullName);

        if (resourceStream is null)
        {
            var existingResources = thisAssembly.GetManifestResourceNames();

            throw new ArgumentException($"Could not find embedded resource '{resourceFullName}'. Available names: {string.Join(", ", existingResources)}");
        }

        using var reader = new StreamReader(resourceStream, Encoding.UTF8);

        return reader.ReadToEnd();
    }
}
