using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddPrimitively(this IServiceCollection services, params IPrimitiveRepository[] repositories)
    {
        var options = new PrimitivelyOptions();

        if (repositories != null)
        {
            foreach (var repository in repositories)
            {
                options.Registry.Add(repository);
            }
        }

        return new PrimitivelyConfigurator(services, options);
    }

    public static PrimitivelyConfigurator AddPrimitively(this IServiceCollection services, Action<PrimitivelyOptions> optionsAction)
    {
        var options = new PrimitivelyOptions();
        optionsAction.Invoke(options);

        return new PrimitivelyConfigurator(services, options);
    }
}
