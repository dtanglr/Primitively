namespace Primitively.MongoDB.Bson.Serialization;

public static class BsonSerializerOptionsExtensions
{
    public static Type GetSerializerType(this IBsonSerializerOptions options, Type primitiveType)
    {
        // Construct a Bson serializer for the given Primitively type using the configured serializer
        var serializerType = options.SerializerType;

        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;
    }
}
