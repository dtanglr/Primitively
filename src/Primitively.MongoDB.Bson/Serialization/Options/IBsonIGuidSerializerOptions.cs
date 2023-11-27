using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public interface IBsonIGuidSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : IBsonSerializerOptions
{
    GuidRepresentation GuidRepresentation { get; internal set; }
}
