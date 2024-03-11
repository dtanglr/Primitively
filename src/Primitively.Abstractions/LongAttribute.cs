namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="ILong"/> type that encapsulates a <see cref="long"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Long attribute to source generate a Primitively <see cref="ILong"/> type.
/// <code>
/// [Long]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Long(Minimum = 0)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Long(Minimum = 1000, Maximum = 2000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class LongAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ILong"/> type.
    /// </summary>
    /// <value>
    /// The default value is -9,223,372,036,854,775,807. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new long Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ILong"/> type.
    /// </summary>
    /// <value>
    /// The default value is 9,223,372,036,854,775,807. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new long Maximum { get; set; }
}
