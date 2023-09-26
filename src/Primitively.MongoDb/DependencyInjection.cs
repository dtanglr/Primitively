using Primitively.Configuration;
using Primitively.MongoDb.Bson.Serialization;

namespace Primitively.MongoDb;

/// <summary>
/// Primitively Configurator extensions to register MongoDB Bson serializers 
/// for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register MongoDB nullable and non-nullable Bson serializers
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="builderAction">Bson Serializer Builder</param>
    /// <param name="options">Bson Serializer Options</param>
    /// <returns>Configurator</returns>
    public static IPrimitivelyConfigurator UseMongoDB(this IPrimitivelyConfigurator configurator, Action<BsonSerializerBuilder> builderAction, BsonSerializerOptions? options = null)
    {
        builderAction.Invoke(new BsonSerializerBuilder(options ?? new BsonSerializerOptions()));

        return configurator;
    }
}
