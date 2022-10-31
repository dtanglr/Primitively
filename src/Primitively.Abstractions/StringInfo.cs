using System;

namespace Primitively;

public record StringInfo(Type Type, Type ValueType, string? Example, string? Format, string? Pattern, int MinLength, int MaxLength)
    : PrimitiveInfo(DataType.String, Type, ValueType, Example);
