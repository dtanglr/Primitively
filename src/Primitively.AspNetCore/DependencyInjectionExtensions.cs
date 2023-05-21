using Primitively.Configuration;

namespace Primitively.AspNetCore;

/// <summary>
/// Primitively Configurator extensions to register AspNet Swagger schema filters and 
/// model binders for Primitively source generated types
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Register ASP.NET related dependencies
    /// </summary>
    /// <param name="configurator">Configurator</param>
    /// <param name="builderAction">AspNet Builder</param>
    /// <returns>Configurator</returns>
    public static IPrimitivelyConfigurator UseAspNet(
        this IPrimitivelyConfigurator configurator,
        Action<IPrimitiveAspNetBuilder> builderAction) =>
            configurator.UseAspNet(builderAction, new PrimitiveAspNetOptions(configurator));

    /// <summary>
    /// Register ASP.NET related dependencies
    /// </summary>
    /// <param name="configurator"><Configurator/param>
    /// <param name="builderAction">AspNet Builder</param>
    /// <param name="options">AspNet Options</param>
    /// <returns>Configurator</returns>
    public static IPrimitivelyConfigurator UseAspNet(
        this IPrimitivelyConfigurator configurator,
        Action<IPrimitiveAspNetBuilder> builderAction,
        PrimitiveAspNetOptions options)
    {
        builderAction.Invoke(options.AspNetBuilder);

        return configurator;
    }
}
