using System;

namespace Primitively;

/// <summary>
/// Represents a record for storing data about a record struct.
/// </summary>
/// <param name="DataType">The data type of the record struct.</param>
/// <param name="Name">The name of the record struct.</param>
/// <param name="NameSpace">The namespace of the record struct.</param>
/// <param name="ParentData">The parent data of the record struct, if any.</param>
/// <remarks>
/// TODO: Break this record into smaller polymorphic records. It's currently used as a dumping ground for all the data that 
/// needs to be passed into the Structs class where source generation takes place.
/// </remarks>
internal record RecordStructData(DataType DataType, string Name, string NameSpace, ParentData? ParentData)
{
    /// <summary>
    /// Gets or sets the length constraint of the record struct data.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the minimum length constraint of the record struct data.
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum length constraint of the record struct data.
    /// </summary>
    public int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the pattern constraint of the record struct data.
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets the example value of the record struct data.
    /// </summary>
    public string? Example { get; set; }

    /// <summary>
    /// Gets or sets the format of the record struct data.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets the interface that the record struct data implements.
    /// </summary>
    public string? Interface { get; set; }

    /// <summary>
    /// Gets or sets the type of the record struct data.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the JSON reader method of the record struct data.
    /// </summary>
    public string? JsonReaderMethod { get; set; }

    /// <summary>
    /// Gets or sets the specifier of the record struct data.
    /// </summary>
    public Specifier? Specifier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the record struct data should implement IValidatableObject.
    /// </summary>
    public bool ImplementIValidatableObject { get; set; }

    /// <summary>
    /// Gets or sets the minimum value constraint of the record struct data.
    /// </summary>
    public object Minimum { get; set; } = 0;

    /// <summary>
    /// Gets or sets the maximum value constraint of the record struct data.
    /// </summary>
    public object Maximum { get; set; } = 0;

    /// <summary>
    /// Gets or sets the number of fractional digits in the value of the source generated Primitively <see cref="IDouble"/> type.
    /// </summary>
    public int? Digits { get; set; }

    /// <summary>
    /// Gets or sets the rounding specification for how to round value of the source generated Primitively <see cref="IDouble"/> type 
    /// if it is midway between two other numbers.
    /// </summary>
    public MidpointRounding? Mode { get; set; }
}
