using Microsoft.Extensions.DependencyInjection;

namespace Primitively.AspNetCore;

[Obsolete($"Use {nameof(DependencyInjectionExtensions)} instead")]
public static class PrimitiveSwaggerGenExtensions
{
    [Obsolete($"Use {nameof(IPrimitiveAspNetBuilder.AddSwaggerSchemaFiltersFor)} on {nameof(IPrimitiveAspNetBuilder)} instead")]
    public static IServiceCollection AddSwaggerGenForPrimitiveTypes(this IServiceCollection services)
    {
        var repos = services
            .BuildServiceProvider()
            .GetServices<IPrimitiveRepository>();

        if (!repos.Any())
        {
            return services;
        }

        return services.AddSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repos.SelectMany(r => r.GetTypes())));
    }

    [Obsolete($"Use {nameof(IPrimitiveAspNetBuilder.AddSwaggerSchemaFiltersFor)} on {nameof(IPrimitiveAspNetBuilder)} instead")]
    public static IServiceCollection AddSwaggerGenForPrimitiveTypes(this IServiceCollection services, IPrimitiveRepository? repository)
    {
        if (repository is null)
        {
            return services;
        }

        return services.AddSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repository!.GetTypes()));
    }

    [Obsolete($"Use {nameof(IPrimitiveAspNetBuilder.AddSwaggerSchemaFiltersFor)} on {nameof(IPrimitiveAspNetBuilder)} instead")]
    public static IServiceCollection AddSwaggerGenForPrimitiveTypes(this IServiceCollection services, IEnumerable<IPrimitiveRepository> repositories)
    {
        if (!repositories.Any())
        {
            return services;
        }

        return services.AddSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repositories.SelectMany(r => r.GetTypes())));
    }
}
