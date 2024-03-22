namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="ISingle"/> type that encapsulates a <see cref="float"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Single attribute to source generate a Primitively <see cref="ISingle"/> type.
/// <code>
/// [Single]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Single(Minimum = 1)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Single(Minimum = 1, Maximum = 100)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class SingleAttribute : NumericAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ISingle"/> type.
    /// </summary>
    /// <value>
    /// The default value is 0. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new float Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ISingle"/> type.
    /// </summary>
    /// <value>
    /// The default value is 255. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new float Maximum { get; set; }
}
