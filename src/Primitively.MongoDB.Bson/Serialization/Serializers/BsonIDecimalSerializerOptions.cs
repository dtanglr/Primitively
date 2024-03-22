using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IDecimal"/> types.
/// </summary>
public class BsonIDecimalSerializerOptions : IBsonConvertibleSerializerOptions<BsonIDecimalSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IDecimal"/> type.
    /// </summary>
    /// <value><see cref="DataType.Decimal"/></value>
    public DataType DataType { get; } = DataType.Decimal;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IDecimal"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Decimal128"/></value>
    public BsonType Representation { get; set; } = BsonType.Decimal128;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IDecimal"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIDecimalSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIDecimalSerializer<>);

    /// <summary>
    /// Gets or sets whether to allow overflow.
    /// </summary>
    public bool AllowOverflow { get; set; }

    /// <summary>
    /// Gets or sets whether to allow truncation.
    /// </summary>
    public bool AllowTruncation { get; set; }

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IDecimal"/> types.
    /// </summary>
    public Func<BsonIDecimalSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
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
