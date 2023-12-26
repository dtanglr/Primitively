using Microsoft.Extensions.DependencyInjection;
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
    /// <param name="options">BsonOptions</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator AddBson(this PrimitivelyConfigurator configurator, Action<BsonOptions>? options = null)
    {
        var registry = configurator.Options.Registry;
        var services = configurator.Services.BuildServiceProvider();
        var manager = services.GetService<IBsonSerializerManager>() ?? new BsonSerializerManager();
        var builder = new BsonOptions(registry, manager);

        options?.Invoke(builder);

        builder.Build();

        return configurator;
    }
}
