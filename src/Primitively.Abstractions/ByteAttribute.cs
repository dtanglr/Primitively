using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Unsigned 8-bit integer primitive value
/// with a default range of: 0 to 255
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class ByteAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public byte Minimum { get; set; }

    public byte Maximum { get; set; }
}
