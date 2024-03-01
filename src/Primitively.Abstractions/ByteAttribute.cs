namespace Primitively;

/// <summary>
/// The <c>ByteAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IByte"/> type that encapsulates a <see cref="byte"/> value.
/// </summary>
/// <example>
/// <code>
/// [Byte]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Byte(Minimum = 1)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Byte(Minimum = 1, Maximum = 100)]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ByteAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IByte"/> type.
    /// The default value is 0.
    /// </summary>
    public new int Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IByte"/> type.
    /// The default value is 255.
    /// </summary>
    public new int Maximum { get; set; }
}
