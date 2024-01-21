using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 16-bit integer primitive value
/// with a default range of: 0 to 65,535
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class UShortAttribute : Attribute, IIntegerAttribute<ushort>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public ushort Minimum { get; set; } = ushort.MinValue;

    /// <inheritdoc/>
    public ushort Maximum { get; set; } = ushort.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
