using Microsoft.AspNetCore.Mvc.ModelBinding;
using Primitively.Configuration;

namespace Primitively.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// This class is a custom model binder provider for Primitively types.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PrimitiveModelBinderProvider"/> class.
/// </remarks>
/// <param name="registry">The registry of Primitively types.</param>
public class PrimitiveModelBinderProvider(PrimitiveRegistry registry) : IModelBinderProvider
{
    private readonly PrimitiveRegistry _registry = registry;

    /// <summary>
    /// Gets the model binder for the specified context.
    /// </summary>
    /// <param name="context">The model binder provider context.</param>
    /// <returns>A model binder for the specified context, or null if the provider cannot supply a binder. The returned binder is a <see cref="PrimitiveModelBinder"/> instance.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;
        if (type is null || !type.IsValueType || !type.IsAssignableTo(typeof(IPrimitive)))
        {
            return null;
        }

        return new PrimitiveModelBinder(_registry);
    }
}
