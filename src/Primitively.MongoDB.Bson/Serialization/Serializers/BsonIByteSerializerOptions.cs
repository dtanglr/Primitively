using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IByte"/> types.
/// </summary>
public class BsonIByteSerializerOptions : IBsonSerializerOptions<BsonIByteSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IByte"/> type.
    /// </summary>
    /// <value><see cref="DataType.Byte"/></value>
    public DataType DataType { get; } = DataType.Byte;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IByte"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Int32"/></value>
    public BsonType Representation { get; set; } = BsonType.Int32;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IByte"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIByteSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIByteSerializer<>);

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IByte"/> types.
    /// </summary>
    public Func<BsonIByteSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
