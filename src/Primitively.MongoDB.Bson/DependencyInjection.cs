using Primitively.Configuration;
using Primitively.MongoDB.Bson.Serialization.Options;

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
    /// <param name="builder">BsonOptions</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator, Action<BsonOptions>? builder = null)
    {
        var registry = configurator.Options.Registry;
        var options = new BsonOptions(registry);
        builder?.Invoke(options);
        options.Build();

        return configurator;
    }
}
