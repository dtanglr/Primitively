using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson.Serialization.Options;

public static class BsonSerializerOptionsExtensions
{
    public static IBsonSerializerOptions<TOptions> AllowOverflow<TOptions>(this IBsonConvertibleSerializerOptions<TOptions> options, bool allowOverflow)
        where TOptions : IBsonSerializerOptions
    {
        options.AllowOverflow = allowOverflow;
        return options;
    }

    public static IBsonSerializerOptions<TOptions> AllowTruncation<TOptions>(this IBsonConvertibleSerializerOptions<TOptions> options, bool allowTruncation)
        where TOptions : IBsonSerializerOptions
    {
        options.AllowTruncation = allowTruncation;
        return options;
    }

    public static IBsonSerializerOptions<TOptions> CreateInstanceWith<TOptions>(this IBsonSerializerOptions<TOptions> options, Func<TOptions, Type, IBsonSerializer> createInstance)
        where TOptions : IBsonSerializerOptions
    {
        options.CreateInstance = createInstance;
        return options;
    }

    public static IBsonSerializerOptions<TOptions> RepresentAs<TOptions>(this IBsonSerializerOptions<TOptions> options, BsonType representation)
        where TOptions : IBsonSerializerOptions
    {
        options.Representation = representation;
        return options;
    }

    public static IBsonSerializerOptions<TOptions> RepresentAs<TOptions>(this IBsonIGuidSerializerOptions<TOptions> options, GuidRepresentation representation)
        where TOptions : IBsonSerializerOptions
    {
        options.GuidRepresentation = representation;
        return options;
    }

    public static IBsonSerializerOptions<TOptions> SerializeWith<TOptions>(this IBsonSerializerOptions<TOptions> options, Type serializerType)
        where TOptions : IBsonSerializerOptions
    {
        options.SerializerType = serializerType;
        return options;
    }
}
