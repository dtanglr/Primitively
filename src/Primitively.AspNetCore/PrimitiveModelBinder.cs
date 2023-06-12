using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Primitively.AspNetCore;

public class PrimitiveModelBinder : IModelBinder
{
    private readonly IEnumerable<IPrimitiveFactory> _factories;

    public PrimitiveModelBinder()
    {
        _factories = new List<IPrimitiveFactory>();
    }

    public PrimitiveModelBinder(IPrimitiveFactory factory)
    {
        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        _factories = new List<IPrimitiveFactory> { factory };
    }

    public PrimitiveModelBinder(IEnumerable<IPrimitiveFactory> factories)
    {
        _factories = factories ?? throw new ArgumentNullException(nameof(factories));
    }

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

    private bool TryCreate(Type type, string? value, out object? result)
    {
        // Try the configured factories first (if any)
        foreach (var factory in _factories)
        {
            if (factory.TryCreate(type, value, out var factoryResult))
            {
                result = factoryResult;
                return true;
            }
        }

        // Try to instantiate via relection/activation instead 
        result = type switch
        {
            _ when type.IsAssignableTo(typeof(IULong)) => ulong.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(ILong)) => long.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IUInt)) => uint.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IInt)) => int.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IUShort)) => ushort.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IShort)) => short.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IByte)) => byte.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(ISByte)) => sbyte.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IDateOnly)) => DateOnly.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IGuid)) => Guid.TryParse(value, out var parsed) ? Activator.CreateInstance(type, parsed) : Activator.CreateInstance(type),
            _ when type.IsAssignableTo(typeof(IString)) => Activator.CreateInstance(type, value),
            _ => null
        };

        return result is not null;
    }
}
