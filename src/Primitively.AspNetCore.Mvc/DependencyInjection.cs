using Microsoft.AspNetCore.Mvc;
using Primitively.AspNetCore.Mvc.ModelBinding;
using Primitively.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The <see cref="DependencyInjection"/> static class provides extension methods to the <see cref="PrimitivelyConfigurator"/> for adding ASP.NET MVC services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds MVC services to the specified <see cref="PrimitivelyConfigurator"/>.
    /// </summary>
    /// <param name="configurator">The <see cref="PrimitivelyConfigurator"/> to which the MVC services are added.</param>
    /// <returns>The same instance of the <see cref="PrimitivelyConfigurator"/> for chaining calls.</returns>
    /// <remarks>
    /// This method configures the MVC options to use the <see cref="PrimitiveModelBinderProvider"/> for Primitively types.
    /// </remarks>
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
