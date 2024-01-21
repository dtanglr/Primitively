using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 16-bit integer primitive value
/// with a default range of: -32,768 to 32,767
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class ShortAttribute : Attribute, IIntegerAttribute<short>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public short Minimum { get; set; } = short.MinValue;

    /// <inheritdoc/>
    public short Maximum { get; set; } = short.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
