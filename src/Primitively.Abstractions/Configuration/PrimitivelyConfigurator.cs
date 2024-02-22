using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

public sealed class PrimitivelyConfigurator
{
    public PrimitivelyConfigurator(IServiceCollection services, PrimitivelyOptions options) =>
        (Services, Options) = (services, options);

    public IServiceCollection Services { get; }

    public PrimitivelyOptions Options { get; }
}
