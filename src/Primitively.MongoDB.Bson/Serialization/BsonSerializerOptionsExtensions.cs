namespace Primitively.MongoDB.Bson.Serialization;

public static class BsonSerializerOptionsExtensions
{
    public static Type GetSerializerType(this IBsonSerializerOptions _, Type primitiveType, Type serializerType)
    {
        // Construct a Bson serializer for the given Primitively type using the options
        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;
    }
}
