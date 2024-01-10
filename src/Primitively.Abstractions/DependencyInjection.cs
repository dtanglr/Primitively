using Primitively.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static PrimitivelyConfigurator AddPrimitively(this IServiceCollection services, Action<PrimitivelyOptions>? optionsAction = null)
    {
        var options = new PrimitivelyOptions();
        optionsAction?.Invoke(options);

        return new PrimitivelyConfigurator(services, options);
    }
}
