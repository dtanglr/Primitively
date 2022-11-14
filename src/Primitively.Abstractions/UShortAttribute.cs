using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates an Unsigned 16-bit integer primitive value
///     with a default range of: 0 to 65,535
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class UShortAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public ushort Minimum { get; set; } = ushort.MinValue;

    public ushort Maximum { get; set; } = ushort.MaxValue;
}
