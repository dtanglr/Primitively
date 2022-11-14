namespace Primitively;

public record IntegerInfo(DataType DataType, Type Type, Type ValueType, string? Example, decimal Minimum, decimal Maximum)
    : PrimitiveInfo(DataType, Type, ValueType, Example);
