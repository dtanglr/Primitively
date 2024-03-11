using Microsoft.Extensions.DependencyInjection;

namespace Primitively.AspNetCore;

/// <summary>
/// This static class provides extension methods to the <see cref="IServiceCollection"/> for adding SwaggerGen for Primitively types.
/// </summary>
public static class PrimitiveSwaggerGenExtensions
{
    /// <summary>
    /// Adds SwaggerGen for Primitively types to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining calls.</returns>
    /// <remarks>
    /// This method configures SwaggerGen to use the <see cref="PrimitiveSchemaFilter"/> for Primitively types.
    /// </remarks>
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

    /// <summary>
    /// Adds SwaggerGen for Primitively types to the specified <see cref="IServiceCollection"/> using a specific repository.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="repository">The repository to use for retrieving Primitively types.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining calls.</returns>
    /// <remarks>
    /// This method configures SwaggerGen to use the <see cref="PrimitiveSchemaFilter"/> for Primitively types from the specified repository.
    /// </remarks>
    public static IServiceCollection AddSwaggerGenForPrimitiveTypes(this IServiceCollection services, IPrimitiveRepository? repository)
    {
        if (repository is null)
        {
            return services;
        }

        return services.AddSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => repository!.GetTypes()));
    }

    /// <summary>
    /// Adds SwaggerGen for Primitively types to the specified <see cref="IServiceCollection"/> using multiple repositories.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="repositories">The repositories to use for retrieving Primitively types.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining calls.</returns>
    /// <remarks>
    /// This method configures SwaggerGen to use the <see cref="PrimitiveSchemaFilter"/> for Primitively types from the specified repositories.
    /// </remarks>
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
