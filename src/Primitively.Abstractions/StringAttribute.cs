﻿namespace Primitively;

/// <summary>
/// This class can be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IString"/> type that encapsulates a <see cref="string"/> value.
/// </summary>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum length constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class StringAttribute : PrimitiveAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringAttribute"/> class with a fixed length.
    /// </summary>
    /// <param name="length">
    /// The fixed length of the string representation of the encapsulated primitive value.
    /// </param>
    public StringAttribute(int length)
    {
        MinLength = length;
        MaxLength = length;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringAttribute"/> class with a specified minimum and maximum length.
    /// </summary>
    /// <param name="minLength">
    /// The minimum length of the string representation of the encapsulated primitive value.
    /// </param>
    /// <param name="maxLength">
    /// The maximum length of the string representation of the encapsulated primitive value.
    /// </param>
    public StringAttribute(int minLength, int maxLength)
    {
        MinLength = minLength;
        MaxLength = maxLength;
    }

    /// <summary>
    /// Gets the minimum length of the string representation of the encapsulated primitive value.
    /// </summary>
    /// <value>
    /// The minimum length of the string representation of the encapsulated primitive value.
    /// </value>
    public int MinLength { get; }

    /// <summary>
    /// Gets the maximum length of the string representation of the encapsulated primitive value.
    /// </summary>
    /// <value>
    /// The maximum length of the string representation of the encapsulated primitive value.
    /// </value>
    public int MaxLength { get; }

#nullable enable
    /// <summary>
    /// Gets or sets the optional regular expression pattern used to ensure that only valid values are encapsulated.
    /// </summary>
    /// <value>
    /// The regular expression pattern used to validate the encapsulated value.
    /// </value>
    public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets an optional example of the string representation of the encapsulated primitive value.
    /// </summary>
    /// <value>
    /// An example of the string representation of the encapsulated primitive value.
    /// </value>
    public string? Example { get; set; }

    /// <summary>
    /// Gets or sets an optional format of the string representation of the encapsulated primitive value.
    /// </summary>
    /// <value>
    /// The format of the string representation of the encapsulated primitive value.
    /// </value>
    public string? Format { get; set; }
#nullable disable
}
