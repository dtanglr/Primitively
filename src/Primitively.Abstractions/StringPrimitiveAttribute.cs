using System;
using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a String primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class StringPrimitiveAttribute : Attribute, IPrimitiveAttribute
{
    /// <summary>
    ///     Make a readonly record struct that encapsulates a string primitive value with a fixed length
    /// </summary>
    /// <param name="length">The fixed length of the string representation of the encapsulated primitive value</param>
    public StringPrimitiveAttribute(
#nullable enable
        string? pattern = StringPrimitive.Default.Pattern,
        string? example = StringPrimitive.Default.Example,
        string? format = StringPrimitive.Default.Format,
#nullable enable
        int length = StringPrimitive.Default.Length)
    {
        Pattern = pattern;
        Example = example;
        Format = format;
        Length = new StringLength(length);
    }

    /// <summary>
    ///     Make a readonly record struct that encapsulates a primitive using the default GUID options
    /// </summary>
    /// <param name="minLength">The minimum length of the string representation of the encapsulated primitive value</param>
    /// <param name="maxLength">The maximum length of the string representation of the encapsulated primitive value</param>
    public StringPrimitiveAttribute(
#nullable enable
        string? pattern = StringPrimitive.Default.Pattern,
        string? example = StringPrimitive.Default.Example,
        string? format = StringPrimitive.Default.Format,
#nullable disable
        int minLength = StringPrimitive.Default.Length,
        int maxLength = StringPrimitive.Default.Length)
    {
        Pattern = pattern;
        Example = example;
        Format = format;
        Length = new StringLengthRange(minLength, maxLength);
    }
#nullable enable
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
#nullable disable
    public Type BackingType => typeof(string);
    public IStringLength Length { get; }
}

public static class StringPrimitive
{
    public struct Default
    {
        public const int Length = 8;
        public const string Pattern = "^[0-9a-fA-F]{8}$";
        public const string Example = "";
        public const string Format = "";
    }
}
