using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for managing BSON serializers.
/// </summary>
public interface IBsonSerializerManager
{
    /// <summary>
    /// Looks up a BSON serializer for the specified type.
    /// </summary>
    /// <param name="type">The type for which to find a serializer.</param>
    /// <returns>The BSON serializer for the specified type.</returns>
    IBsonSerializer LookupSerializer(Type type);

    /// <summary>
    /// Attempts to register a BSON serializer for the specified type.
    /// </summary>
    /// <param name="type">The type for which to register a serializer.</param>
    /// <param name="serializer">The serializer to register.</param>
    /// <returns>true if the serializer was registered successfully; otherwise, false.</returns>
    bool TryRegisterSerializer(Type type, IBsonSerializer serializer);
}
