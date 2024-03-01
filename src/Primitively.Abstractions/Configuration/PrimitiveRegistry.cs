using System.Collections.Concurrent;

namespace Primitively.Configuration;

/// <summary>
/// The PrimitiveRegistry class provides a registry for Primitively types.
/// </summary>
public sealed class PrimitiveRegistry
{
    private readonly ConcurrentDictionary<Type, PrimitiveInfo> _cache = new();
    private readonly ConcurrentBag<Type> _register = new();

    internal PrimitiveRegistry() { }

    /// <summary>
    /// Gets a value indicating whether the registry is empty.
    /// </summary>
    public bool IsEmpty => _cache.IsEmpty;

    /// <summary>
    /// Adds a repository of Primitively types to the registry.
    /// </summary>
    /// <param name="repository">The repository to add.</param>
    public void Add(IPrimitiveRepository repository)
    {
        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        var repositoryType = repository.GetType();

        if (_register.Contains(repositoryType))
        {
            throw new ArgumentException($"Primitive types from '{repositoryType.FullName}' have already been registered", nameof(repository));
        }

        _register.Add(repositoryType);

        foreach (var primitiveInfo in repository.GetTypes())
        {
            _cache.TryAdd(primitiveInfo.Type, primitiveInfo);
        }
    }

    /// <summary>
    /// Converts the registry to a list of Primitively types.
    /// </summary>
    /// <returns>A list of Primitively types.</returns>
    public List<PrimitiveInfo> ToList() => _cache.Values.ToList();

    /// <summary>
    /// Attempts to create an instance of a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="value">The value to encapsulate in the Primitively type.</param>
    /// <param name="primitive">When this method returns, contains the created Primitively type, if the operation succeeded, or null if it did not.</param>
    /// <returns>true if the operation succeeded; otherwise, false.</returns>
    public bool TryCreate(Type type, string? value, out IPrimitive? primitive)
    {
        if (!_cache.TryGetValue(type, out var primitiveInfo))
        {
            primitive = null;
            return false;
        }

        primitive = primitiveInfo.CreateFrom(value);
        return true;
    }

    /// <summary>
    /// Attempts to retrieve the metadata for a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="primitiveInfo">When this method returns, contains the metadata for the Primitively type, if the operation succeeded, or null if it did not.</param>
    /// <returns>true if the operation succeeded; otherwise, false.</returns>
    public bool TryGet(Type type, out PrimitiveInfo? primitiveInfo)
    {
        return _cache.TryGetValue(type, out primitiveInfo);
    }
}
