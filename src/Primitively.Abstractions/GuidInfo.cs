namespace Primitively;

public record GuidInfo(Type Type, Type ValueType, string? Example, Specifier Specifier, int Length)
    : PrimitiveInfo(DataType.Guid, Type, ValueType, Example)
{
    public string Format => Specifier.ToString();
}
