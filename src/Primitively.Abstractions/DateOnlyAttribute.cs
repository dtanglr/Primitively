using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a DateOnly primitive value
/// with default Iso8601 format of yyyy-MM-dd
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class DateOnlyAttribute : Attribute, IPimitivelyAttribute
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }
}
