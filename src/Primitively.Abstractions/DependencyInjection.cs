using Primitively.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods to the <see cref="IServiceCollection"/> for adding Primitively services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Primitively services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="optionsAction">An action delegate to configure the provided <see cref="PrimitivelyOptions"/>.</param>
    /// <returns>A <see cref="PrimitivelyConfigurator"/> that can be used to further configure the Primitively services.</returns>
    public static PrimitivelyConfigurator AddPrimitively(this IServiceCollection services, Action<PrimitivelyOptions>? optionsAction = null)
    {
        var options = new PrimitivelyOptions();
        optionsAction?.Invoke(options);

        return new PrimitivelyConfigurator(services, options);
    }
}
