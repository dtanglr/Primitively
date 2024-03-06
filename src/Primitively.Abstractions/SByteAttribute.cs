namespace Primitively;

/// <summary>
/// The <see cref="SByteAttribute"/> should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="ISByte"/> type that encapsulates an <see cref="sbyte"/> value.
/// </summary>
/// <example>
/// These examples show how to use the SByte attribute to source generate a Primitively <see cref="ISByte"/> type.
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
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class SByteAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ISByte"/> type.
    /// </summary>
    /// <value>
    /// The default value is -128. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new sbyte Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ISByte"/> type.
    /// </summary>
    /// <value>
    /// The default value is 127. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new sbyte Maximum { get; set; }
}
