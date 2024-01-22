namespace PRIMITIVE_NAMESPACE;

public partial class PrimitiveRepository : Primitively.IPrimitiveRepository
{
    private static readonly System.Lazy<System.Collections.Generic.IEnumerable<Primitively.PrimitiveInfo>> _types = new(GetAll, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

    public Primitively.PrimitiveInfo GetType(System.Type type) => _types.Value.Single(t => t.Type.Equals(type));
#nullable enable
    public bool TryGetType(System.Type type, out Primitively.PrimitiveInfo? result)
#nullable disable
    {
        result = _types.Value.SingleOrDefault(t => t.Type.Equals(type));

        return result is not null;
    }

    public System.Collections.Generic.IReadOnlyCollection<Primitively.PrimitiveInfo> GetTypes() => _types.Value.ToList();

    public System.Collections.Generic.IReadOnlyCollection<T> GetTypes<T>() where T : Primitively.PrimitiveInfo => _types.Value.OfType<T>().ToList();

    private static System.Collections.Generic.IEnumerable<Primitively.PrimitiveInfo> GetAll()
    {
PRIMITIVE_REPOSITORY_YIELD_STATEMENTS
    }
}
