using System;
using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a String primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Primitively.ConditionalCompilationSymbol)]
public sealed class StringPrimitiveAttribute : Attribute, IPrimitiveAttribute
{
    /// <summary>
    ///     Make a readonly record struct that encapsulates a string primitive value with a fixed length
    /// </summary>
    /// <param name="length">The fixed length of the string representation of the encapsulated primitive value</param>
    public StringPrimitiveAttribute(int length)
    {
        Length = new StringLength(length);
    }

    /// <summary>
    ///     Make a readonly record struct that encapsulates a primitive using the default GUID options
    /// </summary>
    /// <param name="minLength">The minimum length of the string representation of the encapsulated primitive value</param>
    /// <param name="maxLength">The maximum length of the string representation of the encapsulated primitive value</param>
    public StringPrimitiveAttribute(int minLength, int maxLength)
    {
        Length = new StringLengthRange(minLength, maxLength);
    }

    public Type BackingType => typeof(string);
    public IStringLength Length { get; }
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
}
