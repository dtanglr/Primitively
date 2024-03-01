namespace Primitively;

/// <summary>
/// The <c>LongAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="ILong"/> type that encapsulates an <see cref="long"/> value.
/// </summary>
/// <example>
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
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class LongAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ILong"/> type.
    /// The default value is -9,223,372,036,854,775,807.
    /// </summary>
    public new long Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ILong"/> type.
    /// The default value is 9,223,372,036,854,775,807.
    /// </summary>
    public new long Maximum { get; set; }
}
