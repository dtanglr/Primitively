using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonILongSerializerOptions : IBsonSerializerOptions, IRepresentationConverterOptions
{
    public DataType DataType { get; } = DataType.Long;
    public BsonType Representation { get; set; } = BsonType.Int64;
    public Type SerializerType { get; set; } = typeof(BsonILongSerializer<>);
    public bool AllowOverflow { get; set; }
    public bool AllowTruncation { get; set; }

    public virtual IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        var serializerType = BsonOptions.GetSerializerType<TPrimitive>(SerializerType);

        // Create a Primitively serializer instance
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(
            serializerType,
            Representation,
            new RepresentationConverter(AllowOverflow, AllowTruncation))!;

        return serializerInstance;
    }
}
