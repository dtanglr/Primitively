using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a Signed 64-bit integer primitive value
///     with a default range of: -9,223,372,036,854,775,807 to 9,223,372,036,854,775,807
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class LongAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public long Minimum { get; set; }

    public long Maximum { get; set; }
}
