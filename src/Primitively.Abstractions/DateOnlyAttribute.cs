using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a Date primitive value
///     with default Iso8601 format of yyyy-MM-dd
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class DateOnlyAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }
}
