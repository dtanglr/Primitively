using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;

namespace Primitively.AspNetCore;

public class PrimitiveAspNetBuilder : IPrimitiveAspNetBuilder
{
    private readonly List<PrimitiveInfo> _infos = new();
    private readonly List<IPrimitiveFactory> _factories = new();
    private readonly IPrimitivelyConfigurator _configurator;

    public PrimitiveAspNetBuilder(IPrimitivelyConfigurator configurator)
    {
        _configurator = configurator ?? throw new ArgumentNullException(nameof(configurator));
    }

    public IPrimitiveAspNetBuilder AddModelBindersFor(IPrimitiveFactory primitiveFactory)
    {
        if (!_factories.Contains(primitiveFactory))
        {
            _factories.Add(primitiveFactory);
        }

        return this;
    }

    public IPrimitiveAspNetBuilder AddSwaggerSchemaFilterFor(PrimitiveInfo primitiveInfo)
    {
        AddPrimitiveInfo(primitiveInfo);

        return this;
    }

    public IPrimitiveAspNetBuilder AddSwaggerSchemaFiltersFor(IPrimitiveRepository primitiveRepository)
    {
        foreach (var primitiveInfo in primitiveRepository.GetTypes())
        {
            AddPrimitiveInfo(primitiveInfo);
        }

        return this;
    }

    public void Build()
    {
        _configurator.ConfigureSwaggerGen(options =>
            options.SchemaFilter<PrimitiveSchemaFilter>(() => _infos));

        _configurator.Configure<MvcOptions>(options =>
            options.ModelBinderProviders.Insert(0, new PrimitiveModelBinderProvider(_factories)));
    }

    private void AddPrimitiveInfo(PrimitiveInfo primitiveInfo)
    {
        if (!_infos.Contains(primitiveInfo))
        {
            _infos.Add(primitiveInfo);
        }
    }
}
