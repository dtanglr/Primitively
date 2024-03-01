using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for options that can be used to configure BSON IGuid serializers.
/// </summary>
/// <typeparam name="TOptions">The type of the BSON serializer options.</typeparam>
public interface IBsonIGuidSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : class, IBsonSerializerOptions
{
    /// <summary>
    /// Gets or sets the representation to use for Guid values.
    /// </summary>
    GuidRepresentation GuidRepresentation { get; set; }
}
