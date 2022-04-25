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
    /// <summary>
    ///     Make a readonly record struct that encapsulates a Date primitive value with default Iso8601 format
    /// </summary>
    public DatePrimitiveAttribute(
#nullable enable
        string? pattern = DatePrimitive.Iso8601.Pattern,
        string? example = DatePrimitive.Iso8601.Example,
        string? format = DatePrimitive.Iso8601.Format,
#nullable disable
        int length = DatePrimitive.Iso8601.Length)
    {
        Pattern = pattern;
        Example = example;
        Format = format;
        Length = new StringLength(length);
    }
#nullable enable
    public string? Pattern { get; }
    public string? Example { get; }
    public string? Format { get; }
#nullable disable
#if NET6_0_OR_GREATER
    public Type BackingType => typeof(DateOnly);
#else
    public Type BackingType => typeof(DateTime);
#endif
    public IStringLength Length { get; }
}

public static class DatePrimitive
{
    public struct Iso8601
    {
        public const int Length = 10;
        public const string Pattern = "^(?:(?:(?:(?:(?:[1-9]\\d)(?:0[48]|[2468][048]|[13579][26])|(?:(?:[2468][048]|[13579][26])00))(-)(?:0?2\\1(?:29)))|(?:(?:[1-9]\\d{3})(-)(?:(?:(?:0?[13578]|1[02])\\2(?:31))|(?:(?:0?[13-9]|1[0-2])\\2(?:29|30))|(?:(?:0?[1-9])|(?:1[0-2]))\\2(?:0?[1-9]|1\\d|2[0-8])))))$";
        public const string Example = "2022-01-01";
        public const string Format = "yyyy-MM-dd";
    }
}
