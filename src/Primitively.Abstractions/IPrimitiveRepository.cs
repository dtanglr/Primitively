using System;
using System.Collections.Generic;

namespace Primitively;

public interface IPrimitiveRepository
{
    PrimitiveInfo GetType(Type type);

    bool TryGetType(Type type, out PrimitiveInfo? result);

    IReadOnlyCollection<PrimitiveInfo> GetTypes();

    IReadOnlyCollection<T> GetTypes<T>() where T : PrimitiveInfo;
}
