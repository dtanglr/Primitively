using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

public class PrimitiveModelBinderProvider : IModelBinderProvider
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    public PrimitiveModelBinderProvider()
    {
        _factories = new List<IPrimitiveFactory>();
    }

    public PrimitiveModelBinderProvider(IPrimitiveFactory factory)
    {
        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        _factories = new List<IPrimitiveFactory> { factory };
    }

    public PrimitiveModelBinderProvider(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories ?? throw new ArgumentNullException(nameof(factories));
    }

    public static PrimitiveModelBinderProvider Instance(params IPrimitiveFactory[] factories)
    {
        if (factories == null || !factories.Any())
        {
            return new PrimitiveModelBinderProvider();
        }

        return new PrimitiveModelBinderProvider(factories);
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;
        if (type is null || !type.IsValueType || !type.IsAssignableTo(typeof(IPrimitive)))
        {
            return null;
        }

        if (_factories.Any())
        {
            return new PrimitiveModelBinder(_factories);
        }

        return new PrimitiveModelBinder();
    }
}
