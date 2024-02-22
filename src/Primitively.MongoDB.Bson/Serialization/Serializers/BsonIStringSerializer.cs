using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a serializer for Primitively types that encapsulate a String value.
/// </summary>
public class BsonIStringSerializer<TPrimitive> :
    StructSerializerBase<TPrimitive>,
    IRepresentationConfigurable<BsonIStringSerializer<TPrimitive>> where TPrimitive : struct, IString
{
    private readonly StringSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIStringSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonIStringSerializer(StringSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIStringSerializer{TPrimitive}"/> class.
    /// </summary>
    public BsonIStringSerializer()
    {
        _serializer = StringSerializer.Instance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIStringSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonIStringSerializer(BsonType representation)
    {
        _serializer = new StringSerializer(representation);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="BsonIStringSerializer{TPrimitive}"/> class.
    /// </summary>
    public static BsonIStringSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIStringSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonIStringSerializer<TPrimitive> Create(StringSerializer serializer) => new(serializer);

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
    public BsonIStringSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonIStringSerializer<TPrimitive>(representation);
    }

    // Explicit interface implementations
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
