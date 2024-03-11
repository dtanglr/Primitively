using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

/// <summary>
/// This class is a custom model binder provider for Primitively types.
/// </summary>
public class PrimitiveModelBinderProvider : IModelBinderProvider
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveModelBinderProvider"/> class with a single factory.
    /// </summary>
    /// <param name="factory">The factory to use for creating Primitively types.</param>
    public PrimitiveModelBinderProvider(IPrimitiveFactory factory)
    {
        _factories = new List<IPrimitiveFactory> { factory };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveModelBinderProvider"/> class with multiple factories.
    /// </summary>
    /// <param name="factories">The factories to use for creating Primitively types.</param>
    /// <exception cref="ArgumentNullException">Thrown when the factories parameter is null.</exception>
    public PrimitiveModelBinderProvider(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories ?? throw new ArgumentNullException(nameof(factories));
    }

    /// <summary>
    /// Gets the model binder for the specified context.
    /// </summary>
    /// <param name="context">The model binder provider context.</param>
    /// <returns>A model binder for the specified context, or null if the provider cannot supply a binder. The returned binder is a <see cref="PrimitiveModelBinder"/> instance.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context) =>
        context.Metadata.ModelType.IsAssignableTo(typeof(IPrimitive)) ? new PrimitiveModelBinder(_factories) : null;
}
