using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Defines an interface for a serializer that has a Guid representation.
/// </summary>
public interface IHasGuidRepresentationSerializer
{
    /// <summary>
    /// Gets the representation used for Guid values.
    /// </summary>
    GuidRepresentation GuidRepresentation { get; }
}
