using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Primitively;

namespace PRIMITIVE_NAMESPACE;

public partial class PrimitiveRepository : IPrimitiveRepository
{
    private static readonly Lazy<IEnumerable<PrimitiveInfo>> _types = new(GetAll, LazyThreadSafetyMode.ExecutionAndPublication);

    public PrimitiveInfo GetType(Type type) => _types.Value.Single(t => t.Type.Equals(type));
#nullable enable
    public bool TryGetType(Type type, out PrimitiveInfo? result)
#nullable disable
    {
        result = _types.Value.SingleOrDefault(t => t.Type.Equals(type));

        return result is not null;
    }

    public IReadOnlyCollection<PrimitiveInfo> GetTypes() => _types.Value.ToList();

    public IReadOnlyCollection<T> GetTypes<T>() where T : PrimitiveInfo => _types.Value.OfType<T>().ToList();

    private static IEnumerable<PrimitiveInfo> GetAll()
    {
PRIMITIVE_REPOSITORY_YIELD_STATEMENTS
    }
}
