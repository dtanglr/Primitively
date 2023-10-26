using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;
using Primitively.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore.Mvc;

/// <summary>
/// Primitively Configurator extensions to register MVC model binders for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddMvc(this PrimitivelyConfigurator configurator)
    {
        if (!configurator.Options.Registry.IsEmpty)
        {
            configurator.Services.Configure<MvcOptions>(config =>
                config.ModelBinderProviders.Insert(0, new PrimitiveModelBinderProvider(configurator.Options.Registry)));
        }

        return configurator;
    }
}
