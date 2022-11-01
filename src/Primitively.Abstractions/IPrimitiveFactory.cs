using System;

namespace Primitively;

public interface IPrimitiveFactory
{
    IPrimitive Create(Type type, string? value);

    bool TryCreate(Type type, string? value, out IPrimitive? result);
}
