using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

public static class SwaggerGenOptionsExtensions
{
    public static void AddSchemaFilter(this SwaggerGenOptions options, params IPrimitiveRepository[] repositories)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.SchemaFilter<PrimitiveSchemaFilter>(() => repositories.SelectMany(r => r.GetTypes()));
    }
}
