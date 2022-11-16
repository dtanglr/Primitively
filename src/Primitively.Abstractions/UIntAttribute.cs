using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates an Unsigned 32-bit integer primitive value
///     with a default range of: 0 to 4,294,967,295
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class UIntAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public uint Minimum { get; set; }

    public uint Maximum { get; set; }
}
