using System;
using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a GUID primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class GuidPrimitiveAttribute : Attribute, IPrimitiveAttribute
{
    /// <summary>
    ///     Make a readonly record struct that encapsulates a GUID primitive value with a fixed length
    /// </summary>
    /// <param name="length">The fixed length of the string representation of the encapsulated primitive value</param>
    public GuidPrimitiveAttribute(
        string? pattern = GuidPrimitive.Default.Pattern,
        string? example = GuidPrimitive.Default.Example,
        string? format = GuidPrimitive.Default.Format,
        int length = GuidPrimitive.Default.Length)
    {
        Pattern = pattern;
        Example = example;
        Format = format;
        Length = new StringLength(length);
    }

    public Type BackingType => typeof(Guid);
    public string? Pattern { get; }
    public string? Example { get; }
    public string? Format { get; }
    public IStringLength Length { get; }
}

public static class GuidPrimitive
{
    public struct Default
    {
        public const int Length = 36;
        public const string Pattern = "^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
        public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
        public const string Format = "D";
    }
}
