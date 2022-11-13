using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a Signed 32-bit integer primitive value
///     with a default range of: -2,147,483,648 to 2,147,483,647
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class IntAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }
}
