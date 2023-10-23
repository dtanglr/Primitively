using Microsoft.Extensions.DependencyInjection;
using Primitively.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

public static class PrimitiveSwaggerGenExtensions
{
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

    public static IServiceCollection AddSwaggerGenForPrimitiveTypes(this IServiceCollection services, IPrimitiveRepository? repository)
    {
        if (repository is null)
        {
            return services;
        }

        return services.AddSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repository!.GetTypes()));
    }

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
