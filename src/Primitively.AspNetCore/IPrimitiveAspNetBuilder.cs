namespace Primitively.AspNetCore;

public interface IPrimitiveAspNetBuilder
{
    IPrimitiveAspNetBuilder AddModelBindersFor<T>() where T : class, IPrimitiveFactory, new();
    IPrimitiveAspNetBuilder AddOpenApiSchemaFor<T>() where T : struct, IPrimitive;
    IPrimitiveAspNetBuilder AddOpenApiSchemasFor<T>() where T : class, IPrimitiveRepository, new();
}
