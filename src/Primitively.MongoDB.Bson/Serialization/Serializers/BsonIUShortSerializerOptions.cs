﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

/// <summary>
/// Represents the options used to configure the BSON serialization of Primitively <see cref="IUShort"/> types.
/// </summary>
public class BsonIUShortSerializerOptions : IBsonConvertibleSerializerOptions<BsonIUShortSerializerOptions>
{
    /// <summary>
    /// Gets the <see cref="Primitively.DataType"/> of the Primitively <see cref="IUShort"/> type.
    /// </summary>
    /// <value><see cref="DataType.UShort"/></value>
    public DataType DataType { get; } = DataType.UShort;

    /// <summary>
    /// Gets or sets the <see cref="BsonType"/> used to represent the Primitively <see cref="IUShort"/> type.
    /// </summary>
    /// <value><see cref="BsonType.Int32"/></value>
    public BsonType Representation { get; set; } = BsonType.Int32;

    /// <summary>
    /// Gets or sets the type used to serialize Primitively <see cref="IUShort"/> types.
    /// </summary>
    /// <value><![CDATA[typeof(BsonIUShortSerializer<>)]]></value>
    public Type SerializerType { get; set; } = typeof(BsonIUShortSerializer<>);

    /// <summary>
    /// Gets or sets whether to allow overflow.
    /// </summary>
    public bool AllowOverflow { get; set; }

    /// <summary>
    /// Gets or sets whether to allow truncation.
    /// </summary>
    public bool AllowTruncation { get; set; }

    /// <summary>
    /// Gets or sets the function used to create an instance of the type used to serialize Primitively <see cref="IUShort"/> types.
    /// </summary>
    public Func<BsonIUShortSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = options.GetSerializerType(primitiveType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(
            serializerType,
            options.Representation,
            new RepresentationConverter(options.AllowOverflow, options.AllowTruncation))!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
