using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Represents a serializer whose GUID representation can be configured.
/// </summary>
public interface IGuidRepresentationConfigurable : IHasGuidRepresentationSerializer
{
    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified GUID representation.
    /// </summary>
    /// <param name="representation">The GUID representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    IBsonSerializer WithGuidRepresentation(GuidRepresentation representation);
}

/// <summary>
/// Represents a serializer that has a GuidRepresentation property.
/// </summary>
/// <typeparam name="TSerializer">The type of the serializer.</typeparam>
public interface IGuidRepresentationConfigurable<TSerializer> : IGuidRepresentationConfigurable where TSerializer : IBsonSerializer
{
    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified GUID representation.
    /// </summary>
    /// <param name="representation">The GUID representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    new TSerializer WithGuidRepresentation(GuidRepresentation representation);
}
