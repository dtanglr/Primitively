namespace Primitively;

/// <summary>
/// This class can be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IShort"/> type that encapsulates a <see cref="short"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Short attribute to source generate a Primitively <see cref="IShort"/> type.
/// <code>
/// [Short]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Short(Minimum = 0)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Short(Minimum = -10, Maximum = 10)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ShortAttribute : NumericAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// The default value is -32,768. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </summary>
    /// <value>
    /// The minimum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// </value>
    public new short Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// The default value is 32,767. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </summary>
    /// <value>
    /// The maximum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// </value>
    public new short Maximum { get; set; }
}
