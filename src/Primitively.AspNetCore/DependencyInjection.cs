using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

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

    public static void AddModelBinderProvider(this MvcOptions options, params IPrimitiveFactory[] primitiveFactories)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (primitiveFactories is null)
        {
            throw new ArgumentNullException(nameof(primitiveFactories));
        }

        options.ModelBinderProviders.Insert(0, PrimitiveModelBinderProvider.Instance(primitiveFactories));
    }

    public static void AddSchemaFilter(this SwaggerGenOptions options, params IPrimitiveRepository[] primitiveRepositories)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (primitiveRepositories is null)
        {
            throw new ArgumentNullException(nameof(primitiveRepositories));
        }

        options.SchemaFilter<PrimitiveSchemaFilter>(primitiveRepositories);
    }

    public static PrimitivelyConfigurator ConfigureMvcOptions(this PrimitivelyConfigurator configurator, params IPrimitiveFactory[] primitiveFactories)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        if (primitiveFactories is null)
        {
            throw new ArgumentNullException(nameof(primitiveFactories));
        }

        configurator.Services.Configure<MvcOptions>(config => config.AddModelBinderProvider(primitiveFactories));

        return configurator;
    }

    public static PrimitivelyConfigurator ConfigureSwaggerGenOptions(this PrimitivelyConfigurator configurator, params IPrimitiveRepository[] primitiveRepositories)
    {
        if (configurator is null)
        {
            throw new ArgumentNullException(nameof(configurator));
        }

        if (primitiveRepositories is null)
        {
            throw new ArgumentNullException(nameof(primitiveRepositories));
        }

        configurator.Services.Configure<SwaggerGenOptions>(config => config.AddSchemaFilter(primitiveRepositories));

        return configurator;
    }
}
