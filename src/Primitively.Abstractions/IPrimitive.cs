namespace Primitively;

public interface IPrimitive<T> : IPrimitive
{
    T Value { get; }

    bool HasValue { get; }
}

public interface IPrimitive
{
}
