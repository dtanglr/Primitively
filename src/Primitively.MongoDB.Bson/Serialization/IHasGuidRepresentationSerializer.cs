using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Represents a serializer that has a GuidRepresentation property.
/// </summary>
public interface IHasGuidRepresentationSerializer
{
    /// <summary>
    /// Gets the GUID representation.
    /// </summary>
    GuidRepresentation GuidRepresentation { get; }
}
