namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a GUID primitive value
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class GuidAttribute : Attribute, IPimitivelyAttribute
{
    public GuidAttribute()
    {
        Specifier = Specifier.D;
    }

    public GuidAttribute(Specifier specifier)
    {
        Specifier = specifier;
    }

    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    public Specifier Specifier { get; }
}
