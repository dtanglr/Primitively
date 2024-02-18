namespace PRIMITIVE_NAMESPACE;

public partial class PrimitiveRepository : global::Primitively.IPrimitiveRepository
{
    private static readonly global::System.Lazy<global::System.Collections.Generic.IEnumerable<global::Primitively.PrimitiveInfo>> _types = new(GetAll, global::System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

    public global::Primitively.PrimitiveInfo GetType(global::System.Type type) => _types.Value.Single(t => t.Type.Equals(type));
#nullable enable
    public bool TryGetType(global::System.Type type, out global::Primitively.PrimitiveInfo? result)
#nullable disable
    {
        result = _types.Value.SingleOrDefault(t => t.Type.Equals(type));

        return result is not null;
    }

    public global::System.Collections.Generic.IReadOnlyCollection<global::Primitively.PrimitiveInfo> GetTypes() => _types.Value.ToList();

    public global::System.Collections.Generic.IReadOnlyCollection<T> GetTypes<T>() where T : global::Primitively.PrimitiveInfo => _types.Value.OfType<T>().ToList();

    private static global::System.Collections.Generic.IEnumerable<global::Primitively.PrimitiveInfo> GetAll()
    {
PRIMITIVE_REPOSITORY_YIELD_STATEMENTS
    }
}
