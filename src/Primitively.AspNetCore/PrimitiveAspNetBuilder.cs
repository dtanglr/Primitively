using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;

namespace Primitively.AspNetCore;

public class PrimitiveAspNetBuilder : IPrimitiveAspNetBuilder
{
    private readonly List<PrimitiveInfo> _infos = new();
    private readonly List<IPrimitiveFactory> _factories = new();

    public PrimitiveAspNetBuilder(IPrimitivelyConfigurator configurator)
    {
        Configurator = configurator ?? throw new ArgumentNullException(nameof(configurator));
        Configurator.ConfigureSwaggerGen(options => options.SchemaFilter<PrimitiveSchemaFilter>(() => _infos));
        Configurator.Configure<MvcOptions>(options => options.ModelBinderProviders.Insert(0, new PrimitiveModelBinderProvider(_factories)));
    }

    public IPrimitivelyConfigurator Configurator { get; }

    public IPrimitiveAspNetBuilder AddModelBindersFor(IPrimitiveFactory primitiveFactory)
    {
        if (!_factories.Contains(primitiveFactory))
        {
            _factories.Add(primitiveFactory);
        }

        return this;
    }

    public IPrimitiveAspNetBuilder AddOpenApiSchemaFor(Type primitiveType)
    {
        if (primitiveType is null)
        {
            throw new ArgumentNullException(nameof(primitiveType));
        }

        if (!primitiveType.IsAssignableTo(typeof(IPrimitive)))
        {
            throw new ArgumentException($"The provided type does not implement: {nameof(IPrimitive)}", nameof(primitiveType));
        }

        var primitiveInfo = GetPrimitiveInfo(primitiveType);

        AddPrimitiveInfo(primitiveInfo);

        return this;
    }

    public IPrimitiveAspNetBuilder AddOpenApiSchemaFor(PrimitiveInfo primitiveInfo)
    {
        AddPrimitiveInfo(primitiveInfo);

        return this;
    }

    public IPrimitiveAspNetBuilder AddOpenApiSchemasFor(IPrimitiveRepository primitiveRepository)
    {
        foreach (var primitiveInfo in primitiveRepository.GetTypes())
        {
            AddPrimitiveInfo(primitiveInfo);
        }

        return this;
    }

    private void AddPrimitiveInfo(PrimitiveInfo primitiveInfo)
    {
        if (!_infos.Contains(primitiveInfo))
        {
            _infos.Add(primitiveInfo);
        }
    }

    private static PrimitiveInfo GetPrimitiveInfo(Type primitiveType)
    {
        var primitiveRepositoryTypeName = $"{primitiveType.Assembly.GetName().Name}.{Constants.PrimitiveRepository}";
        var primitiveRepositoryType = primitiveType.Assembly.GetType(primitiveRepositoryTypeName, true);
        var primitiveRepository = Activator.CreateInstance(primitiveRepositoryType!) as IPrimitiveRepository;
        var primitiveInfo = primitiveRepository!.GetType(primitiveType);

        return primitiveInfo;
    }
}
