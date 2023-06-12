using Primitively.Configuration;

namespace Primitively.MongoDb;

/// <summary>
/// Primitively Configurator extensions to register MongoDB Bson serializers 
/// for Primitively source generated types
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Register MongoDB nullable and non-nullable Bson serializers
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="builderAction">Bson Serializer Builder</param>
    /// <returns>Configurator</returns>
    public static IPrimitivelyConfigurator UseMongoDB(this IPrimitivelyConfigurator configurator, Action<IPrimitiveBsonSerializerBuilder> builderAction)
    {
        builderAction.Invoke(new PrimitiveBsonSerializerBuilder());

        return configurator;
    }
}
