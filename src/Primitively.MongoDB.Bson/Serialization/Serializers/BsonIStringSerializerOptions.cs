using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IString"/> types.
/// </summary>
public class BsonIStringSerializerOptions : IBsonSerializerOptions<BsonIStringSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IString"/> type.
    /// </summary>
    /// <value><see cref="DataType.String"/></value>
    public DataType DataType { get; } = DataType.String;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IString"/> type.
    /// </summary>
    /// <value><see cref="BsonType.String"/></value>
    public BsonType Representation { get; set; } = BsonType.String;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IString"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIStringSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIStringSerializer<>);

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IString"/> types.
    /// </summary>
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
