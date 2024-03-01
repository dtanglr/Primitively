using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for configuring the Guid representation in BSON serializers of a specific type.
/// </summary>
/// <typeparam name="TSerializer">The type of the BSON serializer.</typeparam>
public interface IGuidRepresentationConfigurable<out TSerializer> : IGuidRepresentationConfigurable where TSerializer : IBsonSerializer
{
    /// <summary>
    /// Configures the BSON serializer with the specified Guid representation.
    /// </summary>
    /// <param name="representation">The Guid representation to use.</param>
    /// <returns>A BSON serializer of the specified type, configured with the specified Guid representation.</returns>
    new TSerializer WithGuidRepresentation(GuidRepresentation representation);
}
