using MongoDB.Bson;

namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonIGuidSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : class, IBsonSerializerOptions
{
    GuidRepresentation GuidRepresentation { get; set; }
}
