using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IGuid"/> types.
/// </summary>
public class BsonIGuidSerializerOptions : IBsonIGuidSerializerOptions<BsonIGuidSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IGuid"/> type.
    /// </summary>
    /// <value><see cref="DataType.Guid"/></value>
    public DataType DataType { get; } = DataType.Guid;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IGuid"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Binary"/></value>
    public BsonType Representation { get; set; } = BsonType.Binary;

    /// <summary>
    /// Gets or sets the <see cref="global::MongoDB.Bson.GuidRepresentation"/> used to represent the Primitively <see cref="IGuid"/> type.
    /// </summary>
    public GuidRepresentation GuidRepresentation { get; set; } = BsonDefaults.GuidRepresentation;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IGuid"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIGuidSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIGuidSerializer<>);

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IGuid"/> types.
    /// </summary>
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
