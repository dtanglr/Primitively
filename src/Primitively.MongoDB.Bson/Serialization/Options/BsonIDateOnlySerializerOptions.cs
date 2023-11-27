using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIDateOnlySerializerOptions : IBsonSerializerOptions
{
    public DataType DataType { get; } = DataType.DateOnly;
    public BsonType Representation { get; set; } = BsonType.DateTime;
    public Type SerializerType { get; set; } = typeof(BsonIDateOnlySerializer<>);

    public virtual IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        var serializerType = BsonOptions.GetSerializerType<TPrimitive>(SerializerType);

        // Create a Primitively serializer instance
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, Representation)!;

        return serializerInstance;
    }
}
