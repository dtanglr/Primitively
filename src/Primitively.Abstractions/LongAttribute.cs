using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 64-bit integer primitive value
/// with a default range of: -9,223,372,036,854,775,807 to 9,223,372,036,854,775,807
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class LongAttribute : Attribute, IIntegerAttribute<long>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public long Minimum { get; set; } = long.MinValue;

    /// <inheritdoc/>
    public long Maximum { get; set; } = long.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
