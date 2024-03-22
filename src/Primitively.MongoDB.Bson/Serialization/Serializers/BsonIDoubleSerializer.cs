using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a BSON serializer for Primitively <see cref="IDouble"/> types that encapsulate <see cref="decimal"/> values.
/// </summary>
public class BsonIDoubleSerializer<TPrimitive> :
    StructSerializerBase<TPrimitive>,
    IRepresentationConfigurable<BsonIDoubleSerializer<TPrimitive>>,
    IRepresentationConverterConfigurable<BsonIDoubleSerializer<TPrimitive>> where TPrimitive : struct, IDouble
{
    private readonly DoubleSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    public BsonIDoubleSerializer()
    {
        _serializer = new DoubleSerializer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonIDoubleSerializer(BsonType representation)
    {
        _serializer = new DoubleSerializer(representation);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <param name="converter">The converter.</param>
    public BsonIDoubleSerializer(BsonType representation, RepresentationConverter converter)
    {
        _serializer = new DoubleSerializer(representation, converter);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonIDoubleSerializer(DoubleSerializer serializer)
    {
        _serializer = serializer;
    }
    /// <summary>
    /// Gets a cached instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    public static BsonIDoubleSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the converter.
    /// </summary>
    public RepresentationConverter Converter => _serializer.Converter;

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDoubleSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonIDoubleSerializer<TPrimitive> Create(DoubleSerializer serializer) => new(serializer);

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
    public BsonIDoubleSerializer<TPrimitive> WithConverter(RepresentationConverter converter)
    {
        if (converter == _serializer.Converter)
        {
            return this;
        }

        return new BsonIDoubleSerializer<TPrimitive>(_serializer.Representation, converter);
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
    public BsonIDoubleSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonIDoubleSerializer<TPrimitive>(representation, _serializer.Converter);
    }
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
