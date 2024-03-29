﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IDateOnly"/> types.
/// </summary>
public class BsonIDateOnlySerializerOptions : IBsonSerializerOptions<BsonIDateOnlySerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IDateOnly"/> type.
    /// </summary>
    /// <value><see cref="DataType.DateOnly"/></value>
    public DataType DataType { get; } = DataType.DateOnly;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IDateOnly"/> type.
    /// </summary>
    /// <value><see cref="BsonType.DateTime"/></value>
    public BsonType Representation { get; set; } = BsonType.DateTime;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IDateOnly"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIDateOnlySerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIDateOnlySerializer<>);

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IDateOnly"/> types.
    /// </summary>
    public Func<BsonIDateOnlySerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(serializerType, options.Representation)!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
