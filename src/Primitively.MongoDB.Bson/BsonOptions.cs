using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson;

public class BsonOptions
{
    private readonly PrimitiveRegistry _registry;
    private readonly IBsonSerializerManager _manager;
    private readonly Dictionary<DataType, IBsonSerializerOptions> _options = new(GetAll().ToDictionary(o => o.DataType, o => o));
    private readonly Dictionary<Type, DataType> _primitiveTypes = [];

    internal BsonOptions(PrimitiveRegistry registry, IBsonSerializerManager manager)
    {
        _registry = registry;
        _manager = manager;
    }

    public bool RegisterSerializersForEachTypeInRegistry { get; set; } = true;

    private static IEnumerable<IBsonSerializerOptions> GetAll()
    {
        // Initialises the default instancse for each option
        yield return new BsonIByteSerializerOptions();
        yield return new BsonIDateOnlySerializerOptions();
        yield return new BsonIGuidSerializerOptions();
        yield return new BsonIIntSerializerOptions();
        yield return new BsonILongSerializerOptions();
        yield return new BsonISByteSerializerOptions();
        yield return new BsonIShortSerializerOptions();
        yield return new BsonIStringSerializerOptions();
        yield return new BsonIUIntSerializerOptions();
        yield return new BsonIULongSerializerOptions();
        yield return new BsonIUShortSerializerOptions();
    }

    public BsonOptions Configure<TBsonSerializerOptions>(Action<TBsonSerializerOptions> options)
        where TBsonSerializerOptions : class, IBsonSerializerOptions
    {
        var option = GetSerializerOptions<TBsonSerializerOptions>()!;
        options.Invoke(option);

        return this;
    }

    public BsonOptions Register<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        var primitive = new TPrimitive();
        var _ = TryAddPrimitiveType(typeof(TPrimitive), primitive.DataType);

        return this;
    }

    public BsonOptions Register(IPrimitiveRepository repository)
    {
        if (repository is null)
        {
            throw new ArgumentNullException(nameof(repository));
        }

        foreach (var primitiveInfo in repository.GetTypes())
        {
            var _ = TryAddPrimitiveType(primitiveInfo.Type, primitiveInfo.DataType);
        }

        return this;
    }

    internal void Build()
    {
        // If configured, add all the primitive types from the registry
        if (RegisterSerializersForEachTypeInRegistry && !_registry.IsEmpty)
        {
            foreach (var primitiveInfo in _registry.ToList())
            {
                var _ = TryAddPrimitiveType(primitiveInfo.Type, primitiveInfo.DataType);
            }
        }

        // Now generate and register a Bson serializer for each type in the collection
        foreach (var primitiveType in _primitiveTypes)
        {
            RegisterSerializer(primitiveType.Key, primitiveType.Value);
        }
    }

    internal IBsonSerializerOptions GetSerializerOptions(DataType dataType) => _options[dataType];

    internal TOptions GetSerializerOptions<TOptions>() where TOptions : class, IBsonSerializerOptions =>
        (TOptions)_options.Single(o => o.Value is TOptions).Value;

    private bool TryAddPrimitiveType(Type type, DataType dataType)
    {
#if NET6_0_OR_GREATER
        return _primitiveTypes.TryAdd(type, dataType);
#else
        if (_primitiveTypes.ContainsKey(type))
        {
            return false;
        }

        _primitiveTypes.Add(type, dataType);

        return true;
#endif
    }

    private void RegisterSerializer(Type primitiveType, DataType dataType)
    {
        // Retrieve the serializer options for the given dataType
        var serializerOptions = GetSerializerOptions(dataType);

        // Create a Primitively serializer instance
        var serializerInstance = serializerOptions.CreateInstance(primitiveType);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a nullable Primitively serializer instance
        var nullableSerializerInstance = NullableSerializer.Create(serializerInstance);

        // Register a Serializer for the Primitively type
        _manager.TryRegisterSerializer(primitiveType, serializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        _manager.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
    }
}
