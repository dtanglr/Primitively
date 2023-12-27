using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonSerializerOptions<TOptions> : IBsonSerializerOptions
    where TOptions : IBsonSerializerOptions
{
    new Func<TOptions, Type, IBsonSerializer> CreateInstance { get; set; }
}
