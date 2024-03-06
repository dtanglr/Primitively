namespace Primitively;

/// <summary>
/// The <see cref="IPrimitive"/> interface is implemented by all source generated Primitively types.
/// It provides a set of properties to encapsulate a value of a specific .NET primitive type and its metadata.
/// </summary>
public interface IPrimitive
{
    /// <summary>
    /// Gets a flag indicating whether the instance has a valid value.
    /// </summary>
    /// <value>
    /// <c>true</c> if the instance has a valid value; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance.
    /// </remarks>
    public bool HasValue { get; }

    /// <summary>
    /// Gets the .NET primitive type of the value that the instance encapsulates.
    /// </summary>
    /// <value>
    /// The .NET primitive type of the value that the instance encapsulates.
    /// </value>
    public Type ValueType { get; }

    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> enum representation of the type that the instance encapsulates.
    /// </summary>
    /// <value>
    /// The <see cref="Primitively.DataType"/> enum representation of the type that the instance encapsulates.
    /// </value>
    public DataType DataType { get; }

    /// <summary>
    /// Gets the value encapsulated by the instance.
    /// </summary>
    /// <value>
    /// The value encapsulated by the instance.
    /// </value>
    /// <remarks>
    /// Attempts to instantiate a Primitively type with an invalid value will result in a default instance.
    /// </remarks>
    public object Value { get; }
}
