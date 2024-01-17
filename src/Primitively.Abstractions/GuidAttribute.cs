using System.Diagnostics;

namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a GUID primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
[Conditional(Constants.ConditionalCompilationSymbol)]
public sealed class GuidAttribute : Attribute
{
    public GuidAttribute()
    {
        Specifier = Specifier.D;
    }

    public GuidAttribute(Specifier specifier)
    {
        Specifier = specifier;
    }

    public Specifier Specifier { get; }

    public bool ImplementIValidatableObject { get; set; }
}
