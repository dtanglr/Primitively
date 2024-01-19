using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 16-bit integer primitive value
/// with a default range of: -32,768 to 32,767
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class ShortAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public short Minimum { get; set; }

    public short Maximum { get; set; }
}
