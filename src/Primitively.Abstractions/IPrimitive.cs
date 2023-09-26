namespace Primitively;

public interface IPrimitive
{
    bool HasValue { get; }

    Type ValueType { get; }

    DataType DataType { get; }
}

public interface IPrimitive<out T> : IPrimitive
{
    public T Value { get; }
}
