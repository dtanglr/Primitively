namespace Primitively;

public interface IPrimitive
{
    bool HasValue { get; }

    Type ValueType { get; }
}
