using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 64-bit integer primitive value
/// with a default range of: 0 to 18,446,744,073,709,551,615
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class ULongAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public ulong Minimum { get; set; }

    public ulong Maximum { get; set; }
}
