namespace Primitively;

/// <summary>
/// The <c>GuidAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IGuid"/> type that encapsulates a <see cref="Guid"/> value.
/// </summary>
/// <example>
/// <code>
/// [Guid] // e.g. 2c48c152-7cb7-4f51-8f01-704454f36e60
/// public partial record struct Example;
/// </code>
/// <code>
/// [Guid(Specifier.B)] // e.g. {2c48c152-7cb7-4f51-8f01-704454f36e60}
/// public partial record struct Example;
/// </code>
/// <code>
/// [Guid(Specifier.D)] // e.g. 2c48c152-7cb7-4f51-8f01-704454f36e60 (Default)
/// public partial record struct Example;
/// </code>
/// <code>
/// [Guid(Specifier.N)] // e.g. 2c48c1527cb74f518f01704454f36e60
/// public partial record struct Example;
/// </code>
/// <code>
/// [Guid(Specifier.P)] // e.g. (2c48c152-7cb7-4f51-8f01-704454f36e60)
/// public partial record struct Example;
/// </code>
/// <code>
/// [Guid(Specifier.X)] // e.g. {0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class GuidAttribute : PrimitiveAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GuidAttribute"/> class with the default <see cref="Specifier.D"/> specifier.
    /// </summary>
    public GuidAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidAttribute"/> class with the provided specifier.
    /// </summary>
    /// <param name="specifier">The specifier to use when generating the GUID.</param>
    public GuidAttribute(Specifier specifier)
    {
        Specifier = specifier;
    }

    /// <summary>
    /// Gets the specifier used to generate the GUID.
    /// </summary>
    public Specifier Specifier { get; } = Specifier.D;
}
