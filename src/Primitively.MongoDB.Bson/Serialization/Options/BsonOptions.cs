using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public class BsonOptions
{
    private static readonly List<Type> _primitiveTypes = new();

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

    private BsonOptions RegisterSerializerForType(PrimitiveInfo primitiveInfo)
    {
        // Generate a nullable and non-nullable Bson serializer for the Primitively struct
        var options = BsonSerializerOptionsCache.Get(primitiveInfo.DataType);

        RegisterSerializerForType(primitiveInfo.Type, options);

        return this;
    }

    private static void RegisterSerializerForType(Type primitiveType, IBsonSerializerOptions options)
    {
        // Check that Primitive types has not been handled already
        if (_primitiveTypes.Contains(primitiveType))
        {
            return;
        }

        // Add the type to a collection to provide a data source for the above check
        _primitiveTypes.Add(primitiveType);

        // Create a Primitively serializer instance
        var primitiveSerializerInstance = options.CreateInstance(primitiveType);

        // Register a Serializer for the Primitively type
        BsonSerializer.TryRegisterSerializer(primitiveType, primitiveSerializerInstance);

        // Construct a nullable version of the Primitively type
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

        // Create a Nullable Primitively serializer instance
        var nullablePrimitiveSerializerInstance = NullableSerializer.Create(primitiveSerializerInstance);

        // Register a NullableSerializer for a nullable version of the Primitively type
        BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullablePrimitiveSerializerInstance);
    }
}
