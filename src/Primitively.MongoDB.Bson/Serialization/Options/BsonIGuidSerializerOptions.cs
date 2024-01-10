using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIGuidSerializerOptions : IBsonIGuidSerializerOptions<BsonIGuidSerializerOptions>
{
    public DataType DataType { get; } = DataType.Guid;
    public BsonType Representation { get; set; } = BsonType.Binary;
    public GuidRepresentation GuidRepresentation { get; set; } = BsonDefaults.GuidRepresentation;
    public Type SerializerType { get; set; } = typeof(BsonIGuidSerializer<>);
    public Func<BsonIGuidSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Check that the application is using the correct GuidRepresentationMode
        if (BsonDefaults.GuidRepresentationMode == GuidRepresentationMode.V2 && options.GuidRepresentation != BsonDefaults.GuidRepresentation)
        {
            // V3 mode permits a mixture of searchable GUID representations to exist side by side
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        }

        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        object argument = options.Representation == BsonType.String ? BsonType.String : options.GuidRepresentation;
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, argument)!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
