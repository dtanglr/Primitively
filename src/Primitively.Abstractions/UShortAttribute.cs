namespace Primitively;

/// <summary>
/// This class can be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IUShort"/> type that encapsulates an <see cref="ushort"/> value.
/// </summary>
/// <example>
/// These examples show how to use the UShort attribute to source generate a Primitively <see cref="IUShort"/> type.
/// <code>
/// [UShort]
/// public partial record struct Example;
/// 
/// [UShort(Minimum = 10000)]
/// public partial record struct Example;
/// 
/// [UShort(Minimum = 10000, Maximum = 60000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UShortAttribute : NumericAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IUShort"/> type.
    /// </summary>
    /// <value>
    /// The default value is 0. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new ushort Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IUShort"/> type.
    /// </summary>
    /// <value>
    /// The default value is 65,535. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new ushort Maximum { get; set; }
}
