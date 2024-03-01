namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides extension methods for <see cref="IBsonSerializerOptions"/>.
/// </summary>
public static class BsonSerializerOptionsExtensions
{
    /// <summary>
    /// Gets the serializer type for the specified primitive type.
    /// </summary>
    /// <param name="options">The BSON serializer options.</param>
    /// <param name="primitiveType">The primitive type for which to get the serializer type.</param>
    /// <returns>The serializer type for the specified primitive type.</returns>
    public static Type GetSerializerType(this IBsonSerializerOptions options, Type primitiveType)
    {
        // Construct a Bson serializer for the given Primitively type using the configured serializer
        var serializerType = options.SerializerType;

        return serializerType.IsGenericTypeDefinition ? serializerType.MakeGenericType(primitiveType) : serializerType;
    }
}
