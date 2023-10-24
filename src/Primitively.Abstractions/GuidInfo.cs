namespace Primitively;

public record GuidInfo(Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom, Specifier Specifier, int Length)
    : PrimitiveInfo(DataType.Guid, Type, ValueType, Example, CreateFrom)
{
    public string Format => Specifier.ToString();
}
