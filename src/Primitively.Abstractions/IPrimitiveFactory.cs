namespace Primitively;

/// <summary>
/// The IPrimitiveFactory interface provides methods for creating instances of Primitively types.
/// </summary>
public interface IPrimitiveFactory
{
    /// <summary>
    /// Creates an instance of a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="value">The value to encapsulate in the Primitively type.</param>
    /// <returns>An instance of the Primitively type, or null if the value is not valid for the type.</returns>
    IPrimitive? Create(Type type, string? value);

    /// <summary>
    /// Attempts to create an instance of a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="value">The value to encapsulate in the Primitively type.</param>
    /// <param name="result">When this method returns, contains the created Primitively type, if the operation succeeded, or null if it did not.</param>
    /// <returns>true if the operation succeeded; otherwise, false.</returns>
    bool TryCreate(Type type, string? value, out IPrimitive? result);
}
