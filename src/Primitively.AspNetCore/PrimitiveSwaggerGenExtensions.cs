using Microsoft.Extensions.DependencyInjection;

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
        {
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repos.SelectMany(r => r.GetTypes()));
        });
    }
}
