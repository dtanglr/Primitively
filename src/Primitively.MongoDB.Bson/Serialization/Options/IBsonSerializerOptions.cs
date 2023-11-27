using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public interface IBsonSerializerOptions
{
    DataType DataType { get; }
    BsonType Representation { get; set; }
    Type SerializerType { get; set; }
    //Func<IBsonSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; }
    IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive;
}
