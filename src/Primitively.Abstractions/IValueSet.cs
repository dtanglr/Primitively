namespace Primitively;

public interface IValueSet
{
}

public interface IValueSet<T> : IValueSet where T : IPrimitive
{
    T Code { get; }

    string Display { get; }
}
