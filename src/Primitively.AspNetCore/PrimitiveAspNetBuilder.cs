using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

public class PrimitiveAspNetBuilder : IPrimitiveAspNetBuilder
{
    private static readonly ConcurrentDictionary<string, IPrimitiveRepository> _repos = new();

    private readonly List<PrimitiveInfo> _infos = new();
    private readonly List<IPrimitiveFactory> _factories = new();

    public PrimitiveAspNetBuilder(IPrimitivelyConfigurator configurator)
    {
        Configurator = configurator ?? throw new ArgumentNullException(nameof(configurator));
        Configurator.Configure<SwaggerGenOptions>(options => options.SchemaFilter<PrimitiveSchemaFilter>(() => _infos));
        Configurator.Configure<MvcOptions>(options => options.ModelBinderProviders.Insert(0, new PrimitiveModelBinderProvider(_factories)));
    }

    public IPrimitivelyConfigurator Configurator { get; }

    public IPrimitiveAspNetBuilder AddModelBindersFor<T>() where T : class, IPrimitiveFactory, new()
    {
        if (!_factories.Exists(f => f.GetType() == typeof(T)))
        {
            _factories.Add(new T());
        }

        return this;
    }

    public IPrimitiveAspNetBuilder AddOpenApiSchemaFor<T>() where T : struct, IPrimitive
    {
        var primitiveInfo = GetPrimitiveInfo(typeof(T));

        AddPrimitiveInfo(primitiveInfo);

        return this;
    }

    public IPrimitiveAspNetBuilder AddOpenApiSchemasFor<T>() where T : class, IPrimitiveRepository, new()
    {
        var primitiveRepository = new T();

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
        IPrimitiveRepository? primitiveRepository;
        var primitiveRepositoryTypeName = $"{primitiveType.Assembly.GetName().Name}.{Constants.PrimitiveRepository}";

        if (!_repos.ContainsKey(primitiveRepositoryTypeName))
        {
            var primitiveRepositoryType = primitiveType.Assembly.GetType(primitiveRepositoryTypeName, true);
            primitiveRepository = Activator.CreateInstance(primitiveRepositoryType!) as IPrimitiveRepository;
            var _ = _repos.TryAdd(primitiveRepositoryTypeName, primitiveRepository!);
        }
        else
        {
            _repos.TryGetValue(primitiveRepositoryTypeName, out primitiveRepository);
        }

        var primitiveInfo = primitiveRepository!.GetType(primitiveType);

        return primitiveInfo;
    }
}
