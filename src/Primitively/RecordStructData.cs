namespace Primitively;

/// <summary>
/// Represents a record for storing data about a record struct.
/// </summary>
internal record RecordStructData(
    /// <summary>
    /// Gets the data type of the record struct.
    /// </summary>
    DataType DataType,

    /// <summary>
    /// Gets the name of the record struct.
    /// </summary>
    string Name,

    /// <summary>
    /// Gets the namespace of the record struct.
    /// </summary>
    string NameSpace,

    /// <summary>
    /// Gets the parent data of the record struct.
    /// </summary>
    ParentData? ParentData)
{
    /// <summary>
    /// Gets or sets the length of the record struct data.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the minimum length of the record struct data.
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum length of the record struct data.
    /// </summary>
    public int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the pattern of the record struct data.
    /// </summary>
    public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets the example of the record struct data.
    /// </summary>
    public string? Example { get; set; }

    /// <summary>
    /// Gets or sets the format of the record struct data.
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Gets or sets the interface of the record struct data.
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
    /// Gets or sets the minimum value of the record struct data.
    /// </summary>
    public decimal? Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value of the record struct data.
    /// </summary>
    public decimal? Maximum { get; set; }
}
