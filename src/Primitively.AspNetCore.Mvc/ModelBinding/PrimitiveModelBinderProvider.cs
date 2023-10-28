﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Primitively.Configuration;

namespace Primitively.AspNetCore.Mvc.ModelBinding;

public class PrimitiveModelBinderProvider : IModelBinderProvider
{
    private readonly PrimitiveRegistry _registry;

    public PrimitiveModelBinderProvider(PrimitiveRegistry registry)
    {
        _registry = registry;
    }

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