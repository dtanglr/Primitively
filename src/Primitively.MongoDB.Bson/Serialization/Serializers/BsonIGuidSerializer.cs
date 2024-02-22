using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents a serializer for Primitively types that encapsulate a Guid value.
/// </summary>
public class BsonIGuidSerializer<TPrimitive> :
    StructSerializerBase<TPrimitive>,
    IRepresentationConfigurable<BsonIGuidSerializer<TPrimitive>>,
    IGuidRepresentationConfigurable<BsonIGuidSerializer<TPrimitive>> where TPrimitive : struct, IGuid
{
    private readonly GuidSerializer _serializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    private BsonIGuidSerializer(GuidSerializer serializer)
    {
        _serializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <remarks>
    /// This defaults to BsonType.Binary and GuidRepresentation.Standard.
    /// </remarks>
    public BsonIGuidSerializer()
    {
        _serializer = new GuidSerializer();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="representation">The representation.</param>
    public BsonIGuidSerializer(BsonType representation)
    {
        _serializer = new GuidSerializer(representation);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="guidRepresentation">The Guid representation.</param>
    public BsonIGuidSerializer(GuidRepresentation guidRepresentation)
    {
        _serializer = new GuidSerializer(guidRepresentation);
    }

    /// <summary>
    /// Gets a cached instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class with standard representation.
    /// </summary>
    public static BsonIGuidSerializer<TPrimitive> Instance { get; } = new();

    /// <summary>
    /// Gets the Guid representation.
    /// </summary>
    public GuidRepresentation GuidRepresentation => _serializer.GuidRepresentation;

    /// <summary>
    /// Gets the representation.
    /// </summary>
    public BsonType Representation => _serializer.Representation;

    /// <summary>
    /// Initializes a new instance of the <see cref="BsonIGuidSerializer{TPrimitive}"/> class.
    /// </summary>
    /// <param name="serializer">The serializer.</param>
    public static BsonIGuidSerializer<TPrimitive> Create(GuidSerializer serializer) => new(serializer);

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
    /// <param name="representation">The GuidRepresentation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public BsonIGuidSerializer<TPrimitive> WithGuidRepresentation(GuidRepresentation representation)
    {
        if (representation == _serializer.GuidRepresentation)
        {
            return this;
        }

        return new BsonIGuidSerializer<TPrimitive>(representation);
    }

    /// <summary>
    /// Returns a serializer that has been reconfigured with the specified representation.
    /// </summary>
    /// <param name="representation">The representation.</param>
    /// <returns>The reconfigured serializer.</returns>
    public BsonIGuidSerializer<TPrimitive> WithRepresentation(BsonType representation)
    {
        if (representation == _serializer.Representation)
        {
            return this;
        }

        return new BsonIGuidSerializer<TPrimitive>(representation);
    }

    // explicit interface implementations
    IBsonSerializer IRepresentationConfigurable.WithRepresentation(BsonType representation)
    {
        return WithRepresentation(representation);
    }

    IBsonSerializer IGuidRepresentationConfigurable.WithGuidRepresentation(GuidRepresentation representation)
    {
        return WithGuidRepresentation(representation);
    }
}
