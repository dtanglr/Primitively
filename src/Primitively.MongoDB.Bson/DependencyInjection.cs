using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization;

namespace Primitively.MongoDB.Bson;

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
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator)
    {
        if (!configurator.Options.Registry.IsEmpty)
        {
            var builder = new BsonSerializerBuilder(configurator.Options.Registry);
            builder.RegisterSerializers();
        }

        return configurator;
    }

    /// <summary>
    /// Register MongoDB nullable and non-nullable Bson serializers
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="configure">Bson Serializer Builder</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator, Action<BsonSerializerBuilder> configure)
    {
        var builder = new BsonSerializerBuilder(configurator.Options.Registry);
        configure.Invoke(builder);

        if (!configurator.Options.Registry.IsEmpty)
        {
            builder.RegisterSerializers();
        }

        return configurator;
    }
}
