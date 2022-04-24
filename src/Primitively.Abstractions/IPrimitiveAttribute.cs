using System;

namespace Primitively;

public interface IPrimitiveAttribute
{
    IStringLength Length { get; }
#nullable enable
    string? Pattern { get; }
    string? Example { get; }
    string? Format { get; }
#nullable disable
    Type BackingType { get; }
}
