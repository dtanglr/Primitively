namespace Primitively;

public interface IPrimitive
{
    public bool HasValue { get; }

    public Type ValueType { get; }

    public DataType DataType { get; }

    public object Value { get; }
}

public interface IPrimitive<out T> : IPrimitive
{
    public new T Value { get; }
}
