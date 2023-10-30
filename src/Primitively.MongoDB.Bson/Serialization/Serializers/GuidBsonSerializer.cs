using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a serializer for Primitively types that encapsulate a Guid value.
/// </summary>
public class GuidBsonSerializer<TPrimitive> : StructSerializerBase<TPrimitive>, IRepresentationConfigurable<GuidBsonSerializer<TPrimitive>>
    where TPrimitive : struct, IGuid
{
    private readonly GuidSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private GuidBsonSerializer(GuidSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    public GuidBsonSerializer()
    {
        _serializer = GuidSerializer.StandardInstance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public GuidBsonSerializer(BsonType representation)
    {
        _serializer = new GuidSerializer(representation);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="guidRepresentation">The Guid representation.</param>
    public GuidBsonSerializer(GuidRepresentation guidRepresentation)
    {
        _serializer = new GuidSerializer(guidRepresentation);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="IntBsonSerializer{TPrimitive}"/> class with standard representation.
    /// </summary>
    public static GuidBsonSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the Guid representation.
    /// </summary>
    public GuidRepresentation GuidRepresentation => _serializer.GuidRepresentation;

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidBsonSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static GuidBsonSerializer<TPrimitive> Create(GuidSerializer serializer) => new(serializer);

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
    /// Returns a serializer that has been reconfigured with the specified Guid representation.
    /// </summary>
    /// <param name="guidRepresentation">The GuidRepresentation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public GuidBsonSerializer<TPrimitive> WithGuidRepresentation(GuidRepresentation guidRepresentation)
    {
        if (guidRepresentation == _serializer.GuidRepresentation)
        {
            return this;
        }

        return new GuidBsonSerializer<TPrimitive>(guidRepresentation);
    }

    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified representation.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public GuidBsonSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new GuidBsonSerializer<TPrimitive>(representation);
    }

    // explicit interface implementations
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }
}
