namespace Primitively;

public interface INumericInfo
{
    Func<string?, IPrimitive> CreateFrom { get; init; }
    DataType DataType { get; init; }
    string? Example { get; init; }
    Type Type { get; init; }
    Type ValueType { get; init; }
}
