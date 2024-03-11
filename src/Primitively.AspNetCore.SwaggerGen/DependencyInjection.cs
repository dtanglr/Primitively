﻿using Primitively.AspNetCore.SwaggerGen;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This static class provides extension methods to the <see cref="PrimitivelyConfigurator"/> for adding SwaggerGen services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds SwaggerGen services to the specified <see cref="PrimitivelyConfigurator"/>.
    /// </summary>
    /// <param name="configurator">The <see cref="PrimitivelyConfigurator"/> to add services to.</param>
    /// <returns>The same instance of the <see cref="PrimitivelyConfigurator"/> for chaining calls.</returns>
    public static PrimitivelyConfigurator AddSwaggerGen(this PrimitivelyConfigurator configurator)
    {
        if (!configurator.Options.Registry.IsEmpty)
        {
            configurator.Services.Configure<SwaggerGenOptions>(config =>
                config.SchemaFilter<PrimitiveSchemaFilter>(configurator.Options.Registry));
        }

        return configurator;
    }
}
