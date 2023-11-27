﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Primitively.MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public record BsonIULongSerializerOptions : IBsonConvertibleSerializerOptions<BsonIULongSerializerOptions>
{
    public DataType DataType { get; } = DataType.ULong;
    public BsonType Representation { get; set; } = BsonType.Int64;
    public Type SerializerType { get; set; } = typeof(BsonIULongSerializer<>);
    public bool AllowOverflow { get; set; }
    public bool AllowTruncation { get; set; }
    public Func<BsonIULongSerializerOptions, Type, IBsonSerializer> CreateInstance { get; set; } = (options, primitiveType) =>
    {
        // Construct a Bson serializer for the given Primitively type using the options
        var serializerType = BsonOptions.GetSerializerType(primitiveType, options.SerializerType);

        // Create an instance of the serializer
        var serializerInstance = (IBsonSerializer)Activator.CreateInstance(
            serializerType,
            options.Representation,
            new RepresentationConverter(options.AllowOverflow, options.AllowTruncation))!;

        return serializerInstance;
    };

    Func<Type, IBsonSerializer> IBsonSerializerOptions.CreateInstance => (primitiveType) => CreateInstance.Invoke(this, primitiveType);
}
