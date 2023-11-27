using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIGuidSerializerOptions : IBsonSerializerOptions
{
    public DataType DataType { get; } = DataType.Guid;
    public BsonType Representation { get; set; } = BsonType.Binary;
    public GuidRepresentation GuidRepresentation { get; set; } = GuidRepresentation.CSharpLegacy;
    public GuidRepresentationMode GuidRepresentationMode { get; set; } = GuidRepresentationMode.V2;
    public Type SerializerType { get; set; } = typeof(BsonIGuidSerializer<>);

    public virtual IBsonSerializer CreateInstance<TPrimitive>() where TPrimitive : struct, IPrimitive
    {
        // Construct a Primitively serializer of the Primitively type
        var serializerType = BsonOptions.GetSerializerType<TPrimitive>(SerializerType);

        // Create a Primitively serializer instance
        object argument = Representation == BsonType.String ? Representation : GuidRepresentation;
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, argument)!;

        return serializerInstance;
    }
}
