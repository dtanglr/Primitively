using Primitively.Configuration;

namespace Primitively.AspNetCore;

public record PrimitiveAspNetOptions(IPrimitivelyConfigurator Configurator)
{
    public IPrimitiveAspNetBuilder AspNetBuilder { get; init; } = new PrimitiveAspNetBuilder(Configurator);
}
