using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;
using ModelBinderProvider = Primitively.AspNetCore.Mvc.ModelBinding.PrimitiveModelBinderProvider;

namespace Primitively.AspNetCore.Mvc;

/// <summary>
/// Primitively Configurator extensions to register MVC model binders for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddMvc(this PrimitivelyConfigurator configurator)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        configurator.Services.Configure<MvcOptions>(config =>
            config.ModelBinderProviders.Insert(0, new ModelBinderProvider(configurator.Options.Registry)));

        return configurator;
    }
}
