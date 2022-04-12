using System;
using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a Date primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class DatePrimitiveAttribute : Attribute, IPrimitiveAttribute
{
    private const int DefaultLength = 10;
    private const string DefaultPattern = "^(?:(?:(?:(?:(?:[1-9]\\d)(?:0[48]|[2468][048]|[13579][26])|(?:(?:[2468][048]|[13579][26])00))(-)(?:0?2\\1(?:29)))|(?:(?:[1-9]\\d{3})(-)(?:(?:(?:0?[13578]|1[02])\\2(?:31))|(?:(?:0?[13-9]|1[0-2])\\2(?:29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\\2(?:0?[1-9]|1\\d|2[0-8])))))$";
    private const string DefaultExample = "2022-01-01";
    private const string DefaultFormat = "yyyy-MM-dd";

    /// <summary>
    ///     Make a readonly record struct that encapsulates a Date primitive value with default Iso8601 format
    /// </summary>
    public DatePrimitiveAttribute()
    {
        Length = new StringLength(DefaultLength);
    }

    /// <summary>
    ///     Make a readonly record struct that encapsulates a Date primitive value with a fixed length
    /// </summary>
    /// <param name="length">The fixed length of the string representation of the encapsulated primitive value</param>
    public DatePrimitiveAttribute(int length)
    {
        Length = new StringLength(length);
    }

#if NET6_0_OR_GREATER
    public Type BackingType { get; set; } = typeof(DateOnly);
#else
    public Type BackingType { get; set; } = typeof(DateTime);
#endif
    public IStringLength Length { get; }
    public string? Pattern { get; set; } = DefaultPattern;
    public string? Example { get; set; } = DefaultExample;
    public string? Format { get; set; } = DefaultFormat;
}
