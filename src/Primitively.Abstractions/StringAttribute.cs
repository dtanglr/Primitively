namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a String primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class StringAttribute : PrimitiveAttribute
{
    /// <summary>
    /// Make a readonly record struct that encapsulates a
    /// string primitive value with a specified length
    /// </summary>
    /// <param name="length">
    /// The fixed length of the string representation 
    /// of the encapsulated primitive value
    /// </param>
    public StringAttribute(int length)
    {
        MinLength = length;
        MaxLength = length;
    }

    /// <summary>
    /// Make a readonly record struct that encapsulates a
    /// string primitive value with a specified length
    /// </summary>
    /// <param name="minLength">
    /// The minimum length of the string representation 
    /// of the encapsulated primitive value
    /// </param>
    /// <param name="maxLength">
    /// The maximum length of the string representation 
    /// of the encapsulated primitive value
    /// </param>
    public StringAttribute(int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    /// <summary>
    /// The minimum length of the string
    /// </summary>
    public int MinLength { get; }

    /// <summary>
    /// The maximum length of the string
    /// </summary>
    public int MaxLength { get; }

#nullable enable
    /// <summary>
    /// The optional regular expression pattern used to ensure that only valid values are encapsulated 
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// An optional example of the string
    /// </summary>
    public string? Example { get; set; }

    /// <summary>
    /// An optional format of the string
    /// </summary>
    public string? Format { get; set; }
#nullable disable
}
