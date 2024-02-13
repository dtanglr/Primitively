namespace PRIMITIVE_NAMESPACE;

public partial class PrimitiveRepository : Primitively.IPrimitiveRepository
{
    private static readonly global::System.Lazy<global::System.Collections.Generic.IEnumerable<Primitively.PrimitiveInfo>> _types = new(GetAll, global::System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

    public Primitively.PrimitiveInfo GetType(global::System.Type type) => _types.Value.Single(t => t.Type.Equals(type));
#nullable enable
    public bool TryGetType(global::System.Type type, out Primitively.PrimitiveInfo? result)
#nullable disable
    {
        result = _types.Value.SingleOrDefault(t => t.Type.Equals(type));

        return result is not null;
    }

    public global::System.Collections.Generic.IReadOnlyCollection<Primitively.PrimitiveInfo> GetTypes() => _types.Value.ToList();

    public global::System.Collections.Generic.IReadOnlyCollection<T> GetTypes<T>() where T : Primitively.PrimitiveInfo => _types.Value.OfType<T>().ToList();

    private static global::System.Collections.Generic.IEnumerable<Primitively.PrimitiveInfo> GetAll()
    {
PRIMITIVE_REPOSITORY_YIELD_STATEMENTS
    }
}
