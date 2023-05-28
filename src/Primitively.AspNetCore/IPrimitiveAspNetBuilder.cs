namespace Primitively.AspNetCore;

public interface IPrimitiveAspNetBuilder
{
    IPrimitiveAspNetBuilder AddModelBindersFor(IPrimitiveFactory primitiveFactory);
    IPrimitiveAspNetBuilder AddOpenApiSchemaFor(Type primitiveType);
    IPrimitiveAspNetBuilder AddOpenApiSchemaFor(PrimitiveInfo primitiveInfo);
    IPrimitiveAspNetBuilder AddOpenApiSchemasFor(IPrimitiveRepository primitiveRepository);
}
