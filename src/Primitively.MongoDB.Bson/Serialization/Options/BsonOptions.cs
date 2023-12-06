using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public class BsonOptions
{
    private readonly Dictionary<Type, DataType> _primitiveTypes = new();
    private readonly PrimitiveRegistry _registry;

    internal BsonOptions(PrimitiveRegistry registry)
    {
        _registry = registry;
    }

    public bool RegisterSerializersForEachTypeInRegistry { get; set; } = true;

    public static Type GetSerializerType(Type primitiveType, Type serializerType)
    {
        // Construct a Bson serializer for the given Primitively type using the options
        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;
    }

    public BsonOptions BsonIByteSerializer(Action<BsonIByteSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.Byte);
        options.Invoke((BsonIByteSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIDateOnlySerializer(Action<BsonIDateOnlySerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.DateOnly);
        options.Invoke((BsonIDateOnlySerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIGuidSerializer(Action<BsonIGuidSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.Guid);
        options.Invoke((BsonIGuidSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIIntSerializer(Action<BsonIIntSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.Int);
        options.Invoke((BsonIIntSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonILongSerializer(Action<BsonILongSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.Long);
        options.Invoke((BsonILongSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonISByteSerializer(Action<BsonISByteSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.SByte);
        options.Invoke((BsonISByteSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIShortSerializer(Action<BsonIShortSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.Short);
        options.Invoke((BsonIShortSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIStringSerializer(Action<BsonIStringSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.String);
        options.Invoke((BsonIStringSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIUIntSerializer(Action<BsonIUIntSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.UInt);
        options.Invoke((BsonIUIntSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIULongSerializer(Action<BsonIULongSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.ULong);
        options.Invoke((BsonIULongSerializerOptions)option);

        return this;
    }

    public BsonOptions BsonIUShortSerializer(Action<BsonIUShortSerializerOptions> options)
    {
        var option = BsonSerializerOptionsCache.Get(DataType.UShort);
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

    private static void RegisterSerializer(Type primitiveType, DataType dataType)
    {
        // Create a Primitively serializer instance
        var serializerType = BsonSerializerOptionsCache.Get(dataType); // TODO: Update Get method/call to ensure failure handled
        var serializerInstance = serializerType.CreateInstance(primitiveType);

        // Register a Serializer for the Primitively type
        BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a nullable Primitively serializer instance
        var nullableSerializerInstance = NullableSerializer.Create(serializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
    }
}
