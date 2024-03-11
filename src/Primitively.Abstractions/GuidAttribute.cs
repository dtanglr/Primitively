namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IGuid"/> type that encapsulates a <see cref="Guid"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Guid attribute to source generate a Primitively <see cref="IGuid"/> type.
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
    /// <param name="specifier">The specifier to use when generating the <see cref="IGuid"/>.</param>
    public GuidAttribute(Specifier specifier)
    {
        Specifier = specifier;
    }

    /// <summary>
    /// Gets the specifier used to generate the <see cref="IGuid"/>.
    /// </summary>
    /// <value>
    /// The default value is <see cref="Specifier.D"/>. An assigned value should be one of the <see cref="Specifier"/> values.
    /// </value>
    public Specifier Specifier { get; } = Specifier.D;
}
