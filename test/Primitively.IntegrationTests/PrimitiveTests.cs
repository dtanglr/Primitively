using FluentAssertions;

namespace Primitively.IntegrationTests;

public abstract class PrimitiveTests
{
    protected static IEnumerable<Type> Types { get; } = typeof(PrimitiveLibrary)
        .Assembly
        .GetTypes()
        .Where(t => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive)));
}
