namespace Primitively;

/// <summary>
/// The <c>IPrimitive</c> <see langword="interface"/> is implemented by all source generated Primitively types.
/// It provides a set of properties to encapsulate a value of a specific .NET primitive type and it's metadata.
/// </summary>
public interface IPrimitive
{
    /// <summary>
    /// A flag to indicate whether the instance has a valid value.
    /// </summary>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance.
    /// </remarks>
    public bool HasValue { get; }

    /// <summary>
    /// The .NET primitive type of the value that the instance encapsulates.
    /// </summary>
    public Type ValueType { get; }

    /// <summary>
    /// The <see cref="Primitively.DataType"/> <see langword="enum"/> representation of type that the instance encapsulates.
    /// </summary>
    public DataType DataType { get; }

    /// <summary>
    /// The value encapsulated by the instance.
    /// </summary>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance.
    /// </remarks>
    public object Value { get; }
}
