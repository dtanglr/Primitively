namespace Primitively;

/// <summary>
/// Defines a contract for providing metadata about a numeric type.
/// </summary>
public interface INumericInfo
{
    /// <summary>
    /// Gets a function that creates an instance of the numeric type from a string representation.
    /// </summary>
    Func<string?, IPrimitive> CreateFrom { get; init; }

    /// <summary>
    /// Gets the data type of the numeric type.
    /// </summary>
    DataType DataType { get; init; }

    /// <summary>
    /// Gets an example value of the numeric type in string format.
    /// </summary>
    string? Example { get; init; }

    /// <summary>
    /// Gets the .NET type of the numeric type.
    /// </summary>
    Type Type { get; init; }

    /// <summary>
    /// Gets the .NET value type that the numeric type encapsulates.
    /// </summary>
    Type ValueType { get; init; }
}
