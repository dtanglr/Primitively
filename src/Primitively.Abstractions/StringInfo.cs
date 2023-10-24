namespace Primitively;

public record StringInfo(Type Type, Type ValueType, string? Example, Func<string?, IPrimitive> CreateFrom, string? Format, string? Pattern, int MinLength, int MaxLength)
    : PrimitiveInfo(DataType.String, Type, ValueType, Example, CreateFrom);
