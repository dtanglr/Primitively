using Primitively.Configuration;

namespace Primitively.AspNetCore;

/// <summary>
/// Primitively Configurator extensions to register AspNet Swagger schema filters and 
/// model binders for Primitively source generated types
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register ASP.NET related dependencies
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="builderAction">AspNet Builder</param>
    /// <returns>Configurator</returns>
    public static PrimitivelyConfigurator UseAspNet(this PrimitivelyConfigurator configurator, Action<PrimitiveAspNetBuilder>? builderAction = null)
    {
        var builder = new PrimitiveAspNetBuilder(configurator);

        builderAction?.Invoke(builder);

        return configurator;
    }
}
