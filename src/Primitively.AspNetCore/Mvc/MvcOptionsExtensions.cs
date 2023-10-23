using Microsoft.AspNetCore.Mvc;

namespace Primitively.AspNetCore.Mvc;

public static class MvcOptionsExtensions
{
    public static void AddModelBinderProvider(this MvcOptions options, params IPrimitiveFactory[] factories)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        options.ModelBinderProviders.Insert(0, PrimitiveModelBinderProvider.Instance(factories));
    }
}
