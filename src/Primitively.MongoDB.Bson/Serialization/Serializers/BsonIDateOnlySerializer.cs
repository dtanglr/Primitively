using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a serializer for Primitively types that encapsulate a DateOnly value.
/// </summary>
public class BsonIDateOnlySerializer<TPrimitive> : StructSerializerBase<TPrimitive>, IRepresentationConfigurable<BsonIDateOnlySerializer<TPrimitive>>
    where TPrimitive : struct, IDateOnly
{
    private readonly DateTimeSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDateOnlySerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonIDateOnlySerializer(DateTimeSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDateOnlySerializer{TPrimitive}"/> class.
    /// </summary>
    public BsonIDateOnlySerializer()
    {
        _serializer = DateTimeSerializer.DateOnlyInstance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDateOnlySerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonIDateOnlySerializer(BsonType representation)
    {
        _serializer = new DateTimeSerializer(true, representation);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="BsonIDateOnlySerializer{TPrimitive}"/> class.
    /// </summary>
    public static BsonIDateOnlySerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIDateOnlySerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonIDateOnlySerializer<TPrimitive> Create(DateTimeSerializer serializer) => new(serializer);

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
    public BsonIDateOnlySerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonIDateOnlySerializer<TPrimitive>(representation);
    }

    // explicit DateOnlyerface implementations
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
