using System.Collections.Concurrent;

namespace Primitively.Configuration;

public sealed class PrimitiveRegistry
{
    private readonly ConcurrentDictionary<Type, PrimitiveInfo> _cache = new();
    private readonly ConcurrentBag<Type> _register = new();

    internal PrimitiveRegistry() { }

    public bool IsEmpty => _cache.IsEmpty;

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

    public bool TryGet(Type type, out PrimitiveInfo? primitiveInfo)
    {
        return _cache.TryGetValue(type, out primitiveInfo);
    }

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
}
