using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.AspNetCore.Mvc;
using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

/// <summary>
/// Primitively Configurator extensions to register AspNet Swagger schema filters and 
/// model binders for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    public static PrimitivelyConfigurator ConfigureMvcOptions(this PrimitivelyConfigurator configurator, params IPrimitiveFactory[] factories)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        configurator.Services.Configure<MvcOptions>(config => config.AddModelBinderProvider(factories));

        return configurator;
    }

    public static PrimitivelyConfigurator ConfigureSwaggerGenOptions(this PrimitivelyConfigurator configurator, params IPrimitiveRepository[] repositories)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        configurator.Services.Configure<SwaggerGenOptions>(config => config.AddSchemaFilter(repositories));

        return configurator;
    }
}
