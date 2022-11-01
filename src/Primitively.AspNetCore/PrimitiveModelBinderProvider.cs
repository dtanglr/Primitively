using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

public class PrimitiveModelBinderProvider : IModelBinderProvider
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    public PrimitiveModelBinderProvider(IPrimitiveFactory factory)
    {
        _factories = new List<IPrimitiveFactory> { factory };
    }

    public PrimitiveModelBinderProvider(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories;
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType.IsAssignableTo(typeof(IPrimitive)) ? new PrimitiveModelBinder(_factories) : null;
}
