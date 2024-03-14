using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="ISByte"/> types.
/// </summary>
public class BsonISByteSerializerOptions : IBsonSerializerOptions<BsonISByteSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="ISByte"/> type.
    /// </summary>
    /// <value><see cref="DataType.SByte"/></value>
    public DataType DataType { get; } = DataType.SByte;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="ISByte"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Int32"/></value>
    public BsonType Representation { get; set; } = BsonType.Int32;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="ISByte"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonISByteSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonISByteSerializer<>);

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="ISByte"/> types.
    /// </summary>
    public Func<BsonISByteSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
