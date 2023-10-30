using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a serializer for Primitively types that encapsulate a Int value.
/// </summary>
public class IntBsonSerializer<TPrimitive> : StructSerializerBase<TPrimitive>, IRepresentationConfigurable<IntBsonSerializer<TPrimitive>>, IRepresentationConverterConfigurable<IntBsonSerializer<TPrimitive>>
    where TPrimitive : struct, IInt
{
    private readonly Int32Serializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private IntBsonSerializer(Int32Serializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    public IntBsonSerializer()
    {
        _serializer = Int32Serializer.Instance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public IntBsonSerializer(BsonType representation)
    {
        _serializer = new Int32Serializer(representation);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <param name="converter">The converter.</param>
    public IntBsonSerializer(BsonType representation, RepresentationConverter converter)
    {
        _serializer = new Int32Serializer(representation, converter);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    public static IntBsonSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Gets the converter.
    /// </summary>
    public RepresentationConverter Converter => _serializer.Converter;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static IntBsonSerializer<TPrimitive> Create(Int32Serializer serializer) => new(serializer);

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
    public IntBsonSerializer<TPrimitive> WithConverter(RepresentationConverter converter)
    {
        if (converter == _serializer.Converter)
        {
            return this;
        }

        return new IntBsonSerializer<TPrimitive>(_serializer.Representation, converter);
    }

    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified representation.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public IntBsonSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new IntBsonSerializer<TPrimitive>(representation, _serializer.Converter);
    }

    // explicit interface implementations
    IBsonSerializer IRepresentationConverterConfigurable.WithConverter(RepresentationConverter converter)
    {
        return WithConverter(converter);
    }

    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
