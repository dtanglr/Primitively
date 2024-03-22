using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IDouble"/> types.
/// </summary>
public class BsonIDoubleSerializerOptions : IBsonConvertibleSerializerOptions<BsonIDoubleSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <value><see cref="DataType.Double"/></value>
    public DataType DataType { get; } = DataType.Double;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Double"/></value>
    public BsonType Representation { get; set; } = BsonType.Double;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IDouble"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIDoubleSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIDoubleSerializer<>);

    /// <summary>
    /// Gets or sets whether to allow overflow.
    /// </summary>
    public bool AllowOverflow { get; set; }

    /// <summary>
    /// Gets or sets whether to allow truncation.
    /// </summary>
    public bool AllowTruncation { get; set; }

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IDouble"/> types.
    /// </summary>
    public Func<BsonIDoubleSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(
            serializerType,
            options.Representation,
            new RepresentationConverter(options.AllowOverflow, options.AllowTruncation))!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
