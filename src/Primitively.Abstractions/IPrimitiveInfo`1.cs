namespace Primitively;

public interface IPrimitiveInfo<out T> : IPrimitiveInfo where T : PrimitiveInfo
{
    public new T Info { get; }
}
