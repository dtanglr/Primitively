using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for configuring the Guid representation in BSON serializers.
/// </summary>
public interface IGuidRepresentationConfigurable : IHasGuidRepresentationSerializer
{
    /// <summary>
    /// Configures the BSON serializer with the specified Guid representation.
    /// </summary>
    /// <param name="representation">The Guid representation to use.</param>
    /// <returns>A BSON serializer configured with the specified Guid representation.</returns>
    IBsonSerializer WithGuidRepresentation(GuidRepresentation representation);
}
