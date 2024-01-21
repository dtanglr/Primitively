namespace Primitively;

/// <summary>
/// The interface that all source generated Primitively types implement.
/// </summary>
public interface IPrimitive
{
    /// <summary>
    /// Flag indicating the instance has a valid value.
    /// </summary>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance with no value.
    /// </remarks>
    public bool HasValue { get; }

    /// <summary>
    /// The .net type of the value that the instance encapsulates.
    /// </summary>
    public Type ValueType { get; }

    /// <summary>
    /// The <see cref="DataType"/> enum representation of type that the instance encapsulates.
    /// </summary>
    public DataType DataType { get; }

    /// <summary>
    /// The value encapsulated by this instance
    /// </summary>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance with no value.
    /// </remarks>
    public object Value { get; }
}
