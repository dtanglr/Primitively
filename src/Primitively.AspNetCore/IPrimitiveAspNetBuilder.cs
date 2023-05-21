using Microsoft.Extensions.DependencyInjection;

namespace Primitively.AspNetCore;

public interface IPrimitiveAspNetBuilder
{
    IPrimitiveAspNetBuilder AddSwaggerSchemaFilterFor(PrimitiveInfo primitiveInfo);
    IPrimitiveAspNetBuilder AddSwaggerSchemaFiltersFor(IPrimitiveRepository primitiveRepository);
    void Build();
}
