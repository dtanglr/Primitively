using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a String primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class StringAttribute : Attribute
{
    /// <summary>
    ///     Make a readonly record struct that encapsulates a
    ///     string primitive value with a specified length
    /// </summary>
    /// <param name="length">
    ///     The fixed length of the string representation 
    ///     of the encapsulated primitive value
    /// </param>
    public StringAttribute(int length)
    {
        MinLength = length;
        MaxLength = length;
    }

    /// <summary>
    ///     Make a readonly record struct that encapsulates a
    ///     string primitive value with a specified length
    /// </summary>
    /// <param name="minLength">
    ///     The minimum length of the string representation 
    ///     of the encapsulated primitive value
    /// </param>
    /// <param name="maxLength">
    ///     The maximum length of the string representation 
    ///     of the encapsulated primitive value
    /// </param>
    public StringAttribute(int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public int MinLength { get; }
    public int MaxLength { get; }
#nullable enable
    public string? Pattern { get; set; }
    public string? Example { get; set; }
    public string? Format { get; set; }
#nullable disable
    public bool ImplementIValidatableObject { get; set; }
}
