namespace Primitively;

public record IntegerInfo(DataType DataType, Type Type, Type ValueType, string? Example)
    : PrimitiveInfo(DataType, Type, ValueType, Example);
