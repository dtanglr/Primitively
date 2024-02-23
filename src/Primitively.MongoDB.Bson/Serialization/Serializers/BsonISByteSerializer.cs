using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a BSON serializer for Primitively <see cref="ISByte"/> types that encapsulate <see cref="sbyte"/> values.
/// </summary>
public class BsonISByteSerializer<TPrimitive> :
    StructSerializerBase<TPrimitive>,
    IRepresentationConfigurable<BsonISByteSerializer<TPrimitive>> where TPrimitive : struct, ISByte
{
    private readonly SByteSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISByteSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonISByteSerializer(SByteSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISByteSerializer{TPrimitive}"/> class.
    /// </summary>
    public BsonISByteSerializer()
    {
        _serializer = new SByteSerializer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISByteSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonISByteSerializer(BsonType representation)
    {
        _serializer = new SByteSerializer(representation);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="BsonISByteSerializer{TPrimitive}"/> class.
    /// </summary>
    public static BsonISByteSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonISByteSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonISByteSerializer<TPrimitive> Create(SByteSerializer serializer) => new(serializer);

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
    /// Returns a serializer that has been reconfigured with the specified representation.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public BsonISByteSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonISByteSerializer<TPrimitive>(representation);
    }

    // Explicit interface implementations
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
