namespace Primitively;

/// <summary>
/// The <c>ShortAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IShort"/> type that encapsulates an <see cref="short"/> value.
/// </summary>
/// <example>
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
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ShortAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// The default value is -32,768.
    /// </summary>
    public new short Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IShort"/> type.
    /// The default value is 32,767.
    /// </summary>
    public new short Maximum { get; set; }
}
