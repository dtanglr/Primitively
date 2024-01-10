using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Primitively Configurator extensions to register a swagger schema filter for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddSwaggerGen(this PrimitivelyConfigurator configurator)
    {
        if (!configurator.Options.Registry.IsEmpty)
        {
            configurator.Services.Configure<SwaggerGenOptions>(config =>
                config.SchemaFilter<PrimitiveSchemaFilter>(configurator.Options.Registry));
        }

        return configurator;
    }
}
