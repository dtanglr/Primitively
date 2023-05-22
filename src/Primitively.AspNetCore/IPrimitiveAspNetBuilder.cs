namespace Primitively.AspNetCore;

public interface IPrimitiveAspNetBuilder
{
    IPrimitiveAspNetBuilder AddModelBindersFor(IPrimitiveFactory primitiveFactory);
    IPrimitiveAspNetBuilder AddSwaggerSchemaFilterFor(PrimitiveInfo primitiveInfo);
    IPrimitiveAspNetBuilder AddSwaggerSchemaFiltersFor(IPrimitiveRepository primitiveRepository);
    void Build();
}
