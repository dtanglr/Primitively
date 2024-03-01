namespace Primitively;

/// <summary>
/// The <c>UShortAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IUShort"/> type that encapsulates an <see cref="ushort"/> value.
/// </summary>
/// <example>
/// <code>
/// [UShort]
/// public partial record struct Example;
/// </code>
/// <code>
/// [UShort(Minimum = 1)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [UShort(Minimum = 10000, Maximum = 60000)]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UShortAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IUShort"/> type.
    /// The default value is 0.
    /// </summary>
    public new ushort Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IUShort"/> type.
    /// The default value is 65,535.
    /// </summary>
    public new ushort Maximum { get; set; }
}
