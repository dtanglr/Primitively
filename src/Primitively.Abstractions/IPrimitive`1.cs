namespace Primitively;

/// <summary>
/// The <c>IPrimitive</c> <see langword="interface"/> is implemented by all source generated Primitively types.
/// It provides a generic interface for encapsulating a value of a specific .NET primitive type.
/// </summary>
/// <typeparam name="T">The .NET primitive type of the encapsulated value</typeparam>
public interface IPrimitive<out T> : IPrimitive
{
    /// <inheritdoc cref="IPrimitive.Value"/>
    public new T Value { get; }
}
