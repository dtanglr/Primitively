using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// A cache of the default serializer classes
/// </summary>
internal static class BsonSerializerCache
{
    private static readonly Dictionary<DataType, Type> _items = new()
    {
        { DataType.Byte, typeof(BsonIByteSerializer<>) },
        { DataType.DateOnly, typeof(BsonIDateOnlySerializer<>) },
        { DataType.Guid, typeof(BsonIGuidSerializer<>) },
        { DataType.Int, typeof(BsonIIntSerializer<>) },
        { DataType.Long, typeof(BsonILongSerializer<>) },
        { DataType.SByte, typeof(BsonISByteSerializer<>) },
        { DataType.Short, typeof(BsonIShortSerializer<>) },
        { DataType.String, typeof(BsonIStringSerializer<>) },
        { DataType.UInt, typeof(BsonIUIntSerializer<>) },
        { DataType.ULong, typeof(BsonIULongSerializer<>) },
        { DataType.UShort, typeof(BsonIUShortSerializer<>) }
    };

    public static Type Get(DataType dataType) => _items[dataType];

    public static void Set(DataType dataType, Type serializerType)
    {
        if (!serializerType.IsGenericTypeDefinition)
        {
            throw new ArgumentException("The serializer type must be a generic type definition e.g. StringBsonSerializer<>", nameof(serializerType));
        }

        var parameters = serializerType.GetGenericArguments();
        if (parameters.Length > 1)
        {
            throw new ArgumentException("The serializer type should only have one generic type parameter", nameof(serializerType));
        }

        var parameter = parameters[0];
        var interfaces = parameter.GetInterfaces().ToList();
        var isValidImplementation = interfaces.Exists(w => w == dataType switch
        {
            DataType.Byte => typeof(IByte),
            DataType.DateOnly => typeof(IDateOnly),
            DataType.Guid => typeof(IGuid),
            DataType.Int => typeof(IInt),
            DataType.Long => typeof(ILong),
            DataType.SByte => typeof(ISByte),
            DataType.Short => typeof(IShort),
            DataType.String => typeof(IString),
            DataType.UInt => typeof(IUInt),
            DataType.ULong => typeof(IULong),
            DataType.UShort => typeof(IUShort),
            _ => throw new NotImplementedException(),
        });

        if (!isValidImplementation)
        {
            throw new ArgumentException("The serializer type does not implement the expected Primitively type interface for the given data type " +
                "e.g. DataType.Byte should have a serializer type that has generic type constrained to IByte", nameof(serializerType));
        }

        _items[dataType] = serializerType;
    }
}
