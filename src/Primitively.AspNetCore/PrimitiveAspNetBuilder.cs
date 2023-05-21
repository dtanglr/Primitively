using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;

namespace Primitively.AspNetCore;

public class PrimitiveAspNetBuilder : IPrimitiveAspNetBuilder
{
    private readonly List<PrimitiveInfo> _schemaFilterTypes = new();
    private readonly IPrimitivelyConfigurator _configurator;

    public PrimitiveAspNetBuilder(IPrimitivelyConfigurator configurator)
    {
        _configurator = configurator ?? throw new ArgumentNullException(nameof(configurator));
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
            options.SchemaFilter<PrimitiveSchemaFilter>(() => _schemaFilterTypes));
    }

    private void AddPrimitiveInfo(PrimitiveInfo primitiveInfo)
    {
        if (!_schemaFilterTypes.Contains(primitiveInfo))
        {
            _schemaFilterTypes.Add(primitiveInfo);
        }
    }
}
