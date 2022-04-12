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
    private const int DefaultLength = 16;
    private const string DefaultPattern = "^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$";
    private const string DefaultExample = "2c48c152-7cb7-4f51-8f01-704454f36e60";
    private const string DefaultFormat = "D";

    /// <summary>
    ///     Make a readonly record struct that encapsulates a string primitive value with  afixed length
    /// </summary>
    public GuidPrimitiveAttribute()
    {
        Length = new StringLength(DefaultLength);
    }

    /// <summary>
    ///     Make a readonly record struct that encapsulates a string primitive value with  afixed length
    /// </summary>
    /// <param name="length">The fixed length of the string representation of the encapsulated primitive value</param>
    public GuidPrimitiveAttribute(int length)
    {
        Length = new StringLength(length);
    }

    public Type BackingType => typeof(Guid);
    public IStringLength Length { get; }
    public string? Pattern { get; set; } = DefaultPattern;
    public string? Example { get; set; } = DefaultExample;
    public string? Format { get; set; } = DefaultFormat;
}
