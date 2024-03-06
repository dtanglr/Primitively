using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides a concrete implementation of <see cref="IBsonSerializerManager"/> for managing BSON serializers.
/// </summary>
/// <remarks>
/// This class is a wrapper around the <see cref="BsonSerializer"/> class and provides a concrete implementation 
/// of <see cref="IBsonSerializerManager"/> for managing BSON serializers.
/// </remarks>
public class BsonSerializerManager : IBsonSerializerManager
{
    /// <summary>
    /// Looks up a BSON serializer for the specified type.
    /// </summary>
    /// <param name="type">The type for which to find a serializer.</param>
    /// <returns>The BSON serializer for the specified type.</returns>
    public IBsonSerializer LookupSerializer(Type type) =>
        BsonSerializer.LookupSerializer(type);

    /// <summary>
    /// Attempts to register a BSON serializer for the specified type.
    /// </summary>
    /// <param name="type">The type for which to register a serializer.</param>
    /// <param name="serializer">The serializer to register.</param>
    /// <returns>true if the serializer was registered successfully; otherwise, false.</returns>
    public bool TryRegisterSerializer(Type type, IBsonSerializer serializer) =>
        BsonSerializer.TryRegisterSerializer(type, serializer);
}
