using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIStringSerializerOptions : IBsonSerializerOptions
{
    public DataType DataType { get; } = DataType.String;
    public BsonType Representation { get; set; } = BsonType.String;
    public Type SerializerType { get; set; } = typeof(BsonIStringSerializer<>);

    public virtual IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        var serializerType = BsonOptions.GetSerializerType<TPrimitive>(SerializerType);

        // Create a Primitively serializer instance
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, Representation)!;

        return serializerInstance;
    }
}
