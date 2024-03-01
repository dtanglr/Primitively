namespace Primitively;

/// <summary>
/// The <c>ULongAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IULong"/> type that encapsulates an <see cref="ulong"/> value.
/// </summary>
/// <example>
/// <code>
/// [ULong]
/// public partial record struct Example;
/// </code>
/// <code>
/// [ULong(Minimum = 100000000)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [ULong(Minimum = 100000000, Maximum = 200000000)]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ULongAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IULong"/> type.
    /// The default value is 0.
    /// </summary>
    public new ulong Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IULong"/> type.
    /// The default value is 18,446,744,073,709,551,615.
    /// </summary>
    public new ulong Maximum { get; set; }
}
