using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

public record BsonIStringSerializerOptions : IBsonSerializerOptions<BsonIStringSerializerOptions>
{
    public DataType DataType { get; } = DataType.String;
    public BsonType Representation { get; set; } = BsonType.String;
    public Type SerializerType { get; set; } = typeof(BsonIStringSerializer<>);
    public Func<BsonIStringSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
