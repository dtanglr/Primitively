namespace Primitively;

/// <summary>
/// The IPrimitiveRepository interface provides methods for retrieving metadata about Primitively types.
/// </summary>
public interface IPrimitiveRepository
{
    /// <summary>
    /// Retrieves the metadata for a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <returns>The metadata for the Primitively type.</returns>
    PrimitiveInfo GetType(Type type);

    /// <summary>
    /// Attempts to retrieve the metadata for a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="result">When this method returns, contains the metadata for the Primitively type, if the operation succeeded, or null if it did not.</param>
    /// <returns>true if the operation succeeded; otherwise, false.</returns>
    bool TryGetType(Type type, out PrimitiveInfo? result);

    /// <summary>
    /// Retrieves the metadata for all Primitively types.
    /// </summary>
    /// <returns>A collection of the metadata for all Primitively types.</returns>
    IReadOnlyCollection<PrimitiveInfo> GetTypes();

    /// <summary>
    /// Retrieves the metadata for all Primitively types of a specific kind.
    /// </summary>
    /// <typeparam name="T">The type of the metadata to retrieve.</typeparam>
    /// <returns>A collection of the metadata for all Primitively types of the specified kind.</returns>
    IReadOnlyCollection<T> GetTypes<T>() where T : PrimitiveInfo;
}
