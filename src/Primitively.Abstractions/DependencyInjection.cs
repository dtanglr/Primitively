using Primitively.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The <see cref="DependencyInjection"/> static class provides extension methods for the <see cref="IServiceCollection"/> to add Primitively services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Primitively services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the Primitively services are added.</param>
    /// <param name="optionsAction">An optional action delegate to configure the provided <see cref="PrimitivelyOptions"/>.</param>
    /// <returns>A <see cref="PrimitivelyConfigurator"/> that can be used to further configure the Primitively services.</returns>
    /// <remarks>
    /// This method configures the services required by Primitively and returns a <see cref="PrimitivelyConfigurator"/> 
    /// for further configuration. If an action delegate is provided, it is used to configure the <see cref="PrimitivelyOptions"/>.
    /// </remarks>
    public static PrimitivelyConfigurator AddPrimitively(this IServiceCollection services, Action<PrimitivelyOptions>? optionsAction = null)
    {
        var options = new PrimitivelyOptions();
        optionsAction?.Invoke(options);

        return new PrimitivelyConfigurator(services, options);
    }
}
