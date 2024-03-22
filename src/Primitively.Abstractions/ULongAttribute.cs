namespace Primitively;

/// <summary>
/// This class can be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IULong"/> type that encapsulates an <see cref="ulong"/> value.
/// </summary>
/// <example>
/// These examples show how to use the ULong attribute to source generate a Primitively <see cref="IULong"/> type.
/// <code>
/// [ULong]
/// public partial record struct Example;
/// </code>
/// <code>
/// [ULong(Minimum = 100000)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [ULong(Minimum = 20000000, Maximum = 40000000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ULongAttribute : NumericAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IULong"/> type.
    /// </summary>
    /// <value>
    /// The default value is 0. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new ulong Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IULong"/> type.
    /// </summary>
    /// <value>
    /// The default value is 18,446,744,073,709,551,615. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new ulong Maximum { get; set; }
}
