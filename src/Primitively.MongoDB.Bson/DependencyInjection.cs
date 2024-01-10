using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

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
        var services = configurator.Services.BuildServiceProvider();
        var builder = services.GetService<BsonOptions>();

        if (builder == null)
        {
            var registry = configurator.Options.Registry;
            var manager = services.GetService<IBsonSerializerManager>() ?? new BsonSerializerManager();
            builder = new BsonOptions(registry, manager);
            configurator.Services.AddSingleton(typeof(BsonOptions), builder);
        }

        options?.Invoke(builder);
        builder.Build();

        return configurator;
    }
}
