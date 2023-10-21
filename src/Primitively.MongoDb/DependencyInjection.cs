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
    /// <param name="configure">Bson Serializer Builder</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator WithMongoDb(this PrimitivelyConfigurator configurator, Action<BsonSerializerBuilder> configure)
    {
        configure.Invoke(new BsonSerializerBuilder());

        return configurator;
    }
}
