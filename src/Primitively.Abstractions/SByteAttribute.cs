namespace Primitively;

/// <summary>
/// The <c>SByteAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="ISByte"/> type that encapsulates an <see cref="sbyte"/> value.
/// </summary>
/// <example>
/// <code>
/// [SByte]
/// public partial record struct Example;
/// </code>
/// <code>
/// [SByte(Minimum = 0)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [SByte(Minimum = -10, Maximum = 10)]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class SByteAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ISByte"/> type.
    /// The default value is -128.
    /// </summary>
    public new sbyte Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ISByte"/> type.
    /// The default value is 127.
    /// </summary>
    public new sbyte Maximum { get; set; }
}
