namespace Primitively;

public interface IPrimitiveCode
{
}

public interface IPrimitiveCode<T> : IPrimitiveCode where T : IPrimitive
{
    T Code { get; }
}
