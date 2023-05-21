using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPrimitively(this IServiceCollection services, Action<IPrimitivelyConfigurator> configure)
    {
        var configurator = new PrimitivelyConfigurator(services);
        configure.Invoke(configurator);

        return services;
    }
}
