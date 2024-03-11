using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This static class provides extension methods to the <see cref="PrimitivelyConfigurator"/> for adding BSON services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds BSON services to the specified <see cref="PrimitivelyConfigurator"/>.
    /// </summary>
    /// <param name="configurator">The <see cref="PrimitivelyConfigurator"/> to add services to.</param>
    /// <param name="options">A delegate to configure the <see cref="BsonOptions"/>.</param>
    /// <returns>The same instance of the <see cref="PrimitivelyConfigurator"/> for chaining calls.</returns>
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
