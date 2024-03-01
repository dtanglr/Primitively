using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for accessing the Guid representation in BSON serializers.
/// </summary>
public interface IHasGuidRepresentationSerializer
{
    /// <summary>
    /// Gets the representation used for Guid values.
    /// </summary>
    GuidRepresentation GuidRepresentation { get; }
}
