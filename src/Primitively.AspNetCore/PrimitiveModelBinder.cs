using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

/// <summary>
/// This class is a custom model binder for Primitively types.
/// </summary>
public class PrimitiveModelBinder : IModelBinder
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveModelBinder"/> class with a single factory.
    /// </summary>
    /// <param name="factory">The factory to use for creating Primitively types.</param>
    public PrimitiveModelBinder(IPrimitiveFactory factory)
    {
        _factories = new List<IPrimitiveFactory> { factory };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveModelBinder"/> class with multiple factories.
    /// </summary>
    /// <param name="factories">The factories to use for creating Primitively types.</param>
    public PrimitiveModelBinder(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories ?? throw new ArgumentNullException(nameof(factories));
    }

    /// <summary>
    /// Asynchronously attempts to bind a model.
    /// </summary>
    /// <param name="bindingContext">The model binding context.</param>
    /// <returns>A <see cref="Task"/> representing the model binding operation. The task result contains the <see cref="ModelBindingResult"/>.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        // Try to fetch the value of the argument by name
        var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (result == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        // Sets the value for the ModelStateEntry with the specified key
        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result);

        // Create an instance of the Primitive ModelType
        var created = TryCreate(bindingContext.ModelType, result.FirstValue, out var model);

        // Create a ModelBindingResult representing model binding operation outcome
        bindingContext.Result = created
            ? ModelBindingResult.Success(model)
            : ModelBindingResult.Failed();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Attempts to create an instance of a specific Primitively type.
    /// </summary>
    /// <param name="type">The .NET type of the Primitively type.</param>
    /// <param name="value">The string representation of the value to encapsulate in the Primitively type.</param>
    /// <param name="result">When this method returns, contains the created Primitively type, if the operation succeeded, or null if it did not.</param>
    /// <returns><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</returns>
    private bool TryCreate(Type type, string? value, out IPrimitive? result)
    {
        result = null;

        foreach (var factory in _factories)
        {
            var created = factory.TryCreate(type, value, out result);

            if (created)
            {
                return true;
            }
        }

        return false;
    }
}
