namespace Primitively;

public record DateOnlyInfo(Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom, string Format, int Length)
    : PrimitiveInfo(DataType.DateOnly, Type, ValueType, Example, CreateFrom);
