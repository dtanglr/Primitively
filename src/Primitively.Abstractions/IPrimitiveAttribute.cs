using System;

namespace Primitively;

public interface IPrimitiveAttribute
{
    IStringLength Length { get; }
    string? Pattern { get; }
    string? Example { get; }
    string? Format { get; }
    Type BackingType { get; }
}
