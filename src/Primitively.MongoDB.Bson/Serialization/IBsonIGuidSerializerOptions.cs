using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonIGuidSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : IBsonSerializerOptions
{
    GuidRepresentation GuidRepresentation { get; internal set; }
}
