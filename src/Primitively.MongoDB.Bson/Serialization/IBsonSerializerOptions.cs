using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonSerializerOptions
{
    DataType DataType { get; }
    BsonType Representation { get; internal set; }
    Type SerializerType { get; internal set; }
    Func<Type, IBsonSerializer> CreateInstance { get; }
}
