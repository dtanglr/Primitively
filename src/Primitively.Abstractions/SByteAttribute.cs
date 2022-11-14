using System.Diagnostics;

namespace Primitively;

/// <summary>
///     Make a readonly record struct that encapsulates a Signed 8-bit integer primitive value
///     with a default range of: -128 to 127
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class SByteAttribute : Attribute
{
    public bool ImplementIValidatableObject { get; set; }

    public sbyte Minimum { get; set; } = sbyte.MinValue;

    public sbyte Maximum { get; set; } = sbyte.MaxValue;
}
