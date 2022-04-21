namespace Primitively;

public interface IPrimitive
{
}

public interface IPrimitive<T> : IPrimitive
{
    T Value { get; }

    bool HasValue { get; }
}
