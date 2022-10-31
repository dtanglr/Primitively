using System;

namespace Primitively;

public record DateOnlyInfo(Type Type, Type ValueType, string? Example, string Format, int Length)
    : PrimitiveInfo(DataType.DateOnly, Type, ValueType, Example);
