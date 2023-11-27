using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIByteSerializerOptions : IBsonSerializerOptions
{
    public DataType DataType { get; } = DataType.Byte;
    public BsonType Representation { get; set; } = BsonType.Int32;
    public Type SerializerType { get; set; } = typeof(BsonIByteSerializer<>);
    //Func<IBsonSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    //{
    //    // Construct a Primitively serializer of the Primitively type
    //    var serializerType = BsonOptions.GetSerializerType(primitiveType, options.SerializerType);

    //    // Create a Primitively serializer instance
    //    var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

    //    return serializerInstance;
    //};

    public virtual IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        var serializerType = BsonOptions.GetSerializerType<TPrimitive>(SerializerType);

        // Create a Primitively serializer instance
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, Representation)!;

        return serializerInstance;
    }
}
