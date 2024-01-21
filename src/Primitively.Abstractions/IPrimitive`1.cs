namespace Primitively;

/// <summary>
/// The generic interface that all source generated Primitively types implement
/// </summary>
/// <typeparam name="T">The .net type of the encapsulated value</typeparam>
public interface IPrimitive<out T> : IPrimitive
{
    /// <summary>
    /// The value of the Primitively type 
    /// </summary>
    public new T Value { get; }
}
