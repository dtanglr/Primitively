using System.Collections.Concurrent;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public class BsonOptions
{
    private readonly PrimitiveRegistry _registry;
    private readonly ConcurrentDictionary<DataType, IBsonSerializerOptions> _options;
    private readonly Dictionary<Type, DataType> _primitiveTypes = new();

    internal BsonOptions(PrimitiveRegistry registry)
    {
        _registry = registry;
        _options = new(GetAll().ToDictionary(o => o.DataType, o => o));
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

    public IBsonSerializerOptions GetSerializerOptions(DataType dataType) => _options[dataType];

    public BsonOptions BsonIByteSerializer(Action<BsonIByteSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.Byte);
        options.Invoke((BsonIByteSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIDateOnlySerializer(Action<BsonIDateOnlySerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.DateOnly);
        options.Invoke((BsonIDateOnlySerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIGuidSerializer(Action<BsonIGuidSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.Guid);
        options.Invoke((BsonIGuidSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIIntSerializer(Action<BsonIIntSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.Int);
        options.Invoke((BsonIIntSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonILongSerializer(Action<BsonILongSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.Long);
        options.Invoke((BsonILongSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonISByteSerializer(Action<BsonISByteSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.SByte);
        options.Invoke((BsonISByteSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIShortSerializer(Action<BsonIShortSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.Short);
        options.Invoke((BsonIShortSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIStringSerializer(Action<BsonIStringSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.String);
        options.Invoke((BsonIStringSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIUIntSerializer(Action<BsonIUIntSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.UInt);
        options.Invoke((BsonIUIntSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIULongSerializer(Action<BsonIULongSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.ULong);
        options.Invoke((BsonIULongSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIUShortSerializer(Action<BsonIUShortSerializerOptions> options)
    {
        var option = GetSerializerOptions(DataType.UShort);
        options.Invoke((BsonIUShortSerializerOptions)option);

        return this;
    }

    internal void Build()
    {
        if (RegisterSerializersForEachTypeInRegistry && !_registry.IsEmpty)
        {
            foreach (var primitiveInfo in _registry.ToList())
            {
#if NET6_0_OR_GREATER
                _primitiveTypes.TryAdd(primitiveInfo.Type, primitiveInfo.DataType);
#else
                if (!_primitiveTypes.ContainsKey(primitiveInfo.Type))
                {
                    _primitiveTypes.Add(primitiveInfo.Type, primitiveInfo.DataType);
                }
#endif
            }
        }

        foreach (var primitiveType in _primitiveTypes)
        {
            RegisterSerializer(primitiveType.Key, primitiveType.Value);
        }
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
        BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
    }
}
