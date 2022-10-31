﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

public class PrimitiveModelBinder : IModelBinder
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    public PrimitiveModelBinder(IPrimitiveFactory factory)
    {
        _factories = new List<IPrimitiveFactory> { factory };
    }

    public PrimitiveModelBinder(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        // Try to fetch the value of the argument by name
        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (value == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        // Sets the value for the ModelStateEntry with the specified key
        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

        // Create an instance of the Primitive ModelType
        var created = TryCreate(bindingContext.ModelType, value.FirstValue, out var model);

        // Create a ModelBindingResult representing model binding operation outcome
        bindingContext.Result = created
            ? ModelBindingResult.Success(model)
            : ModelBindingResult.Failed();

        return Task.CompletedTask;
    }

    private bool TryCreate(Type type, string value, out IPrimitive? result)
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
