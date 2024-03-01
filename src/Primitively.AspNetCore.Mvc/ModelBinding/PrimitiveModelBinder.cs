using Microsoft.AspNetCore.Mvc.ModelBinding;
using Primitively.Configuration;

namespace Primitively.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// The PrimitiveModelBinder class is a custom model binder for Primitively types.
/// </summary>
public class PrimitiveModelBinder : IModelBinder
{
    private readonly PrimitiveRegistry _registry;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveModelBinder"/> class.
    /// </summary>
    /// <param name="registry">The registry of Primitively types.</param>
    public PrimitiveModelBinder(PrimitiveRegistry registry)
    {
        _registry = registry;
    }

    /// <summary>
    /// Asynchronously attempts to bind a model.
    /// </summary>
    /// <param name="bindingContext">The model binding context.</param>
    /// <returns>A task representing the model binding operation.</returns>
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
        var created = _registry.TryCreate(bindingContext.ModelType, result.FirstValue, out var model);

        // Create a ModelBindingResult representing model binding operation outcome
        bindingContext.Result = created
            ? ModelBindingResult.Success(model)
            : ModelBindingResult.Failed();

        return Task.CompletedTask;
    }
}
