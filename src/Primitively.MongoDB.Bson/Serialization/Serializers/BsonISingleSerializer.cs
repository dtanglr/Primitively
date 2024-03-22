using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a BSON serializer for Primitively <see cref="ISingle"/> types that encapsulate <see cref="decimal"/> values.
/// </summary>
public class BsonISingleSerializer<TPrimitive> :
    StructSerializerBase<TPrimitive>,
    IRepresentationConfigurable<BsonISingleSerializer<TPrimitive>>,
    IRepresentationConverterConfigurable<BsonISingleSerializer<TPrimitive>> where TPrimitive : struct, ISingle
{
    private readonly SingleSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    public BsonISingleSerializer()
    {
        _serializer = new SingleSerializer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonISingleSerializer(BsonType representation)
    {
        _serializer = new SingleSerializer(representation);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <param name="converter">The converter.</param>
    public BsonISingleSerializer(BsonType representation, RepresentationConverter converter)
    {
        _serializer = new SingleSerializer(representation, converter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonISingleSerializer(SingleSerializer serializer)
    {
        _serializer = serializer;
    }
    /// <summary>
    /// Gets a cached instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    public static BsonISingleSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the converter.
    /// </summary>
    public RepresentationConverter Converter => _serializer.Converter;

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISingleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonISingleSerializer<TPrimitive> Create(SingleSerializer serializer) => new(serializer);

    /// <summary>
    /// Deserializes a value.
    /// </summary>
    /// <param name="context">The deserialization context.</param>
    /// <param name="args">The deserialization args.</param>
    /// <returns>A deserialized value.</returns>
    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = _serializer.Deserialize(context, args);

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }

    /// <summary>
    /// Serializes a value.
    /// </summary>
    /// <param name="context">The serialization context.</param>
    /// <param name="args">The serialization args.</param>
    /// <param name="value">The object.</param>
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        _serializer.Serialize(context, args, value.Value);
    }

    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified item serializer.
    /// </summary>
    /// <param name="converter">The converter.</param>
    /// <returns>The reconfigured serializer.</returns>
    public BsonISingleSerializer<TPrimitive> WithConverter(RepresentationConverter converter)
    {
        if (converter == _serializer.Converter)
        {
            return this;
        }

        return new BsonISingleSerializer<TPrimitive>(_serializer.Representation, converter);
    }

    // Explicit interface implementations
    IBsonSerializer IRepresentationConverterConfigurable.WithConverter(RepresentationConverter converter)
    {
        return WithConverter(converter);
    }

    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified representation.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public BsonISingleSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonISingleSerializer<TPrimitive>(representation, _serializer.Converter);
    }
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
