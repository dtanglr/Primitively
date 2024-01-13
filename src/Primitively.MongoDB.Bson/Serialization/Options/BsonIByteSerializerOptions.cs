using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

/// <summary>
/// Represents a class that facilitates the configuration of a serializer for Primitively types that encapsulate a Byte value.
/// </summary>
public record BsonIByteSerializerOptions : IBsonSerializerOptions<BsonIByteSerializerOptions>
{
    /// <inheritdoc/>
    /// <remarks>Returns: <see cref="DataType.Byte"/></remarks>
    public DataType DataType { get; } = DataType.Byte;

    /// <inheritdoc/>
    /// <remarks>Default: <see cref="BsonType.Int32"/></remarks>
    public BsonType Representation { get; set; } = BsonType.Int32;

    /// <inheritdoc/>
    /// <remarks>Default: typeof(BsonIByteSerializer&lt;&gt;)</remarks>
    public Type SerializerType { get; set; } = typeof(BsonIByteSerializer<>);

    /// <inheritdoc/>
    public Func<BsonIByteSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

        return serializerInstance;
    };

    /// <inheritdoc/>
    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
