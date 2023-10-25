using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore.SwaggerGen;

/// <summary>
/// Primitively Configurator extensions to register a swagger schema filter for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddSwaggerGen(this PrimitivelyConfigurator configurator)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        if (!configurator.Options.Registry.IsEmpty)
        {
            configurator.Services.Configure<SwaggerGenOptions>(config =>
                config.SchemaFilter<PrimitiveSchemaFilter>(configurator.Options.Registry));
        }

        return configurator;
    }
}
