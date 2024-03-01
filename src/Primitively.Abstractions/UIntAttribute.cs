namespace Primitively;

/// <summary>
/// The <c>UIntAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IUInt"/> type that encapsulates an <see cref="uint"/> value.
/// </summary>
/// <example>
/// <code>
/// [UInt]
/// public partial record struct Example;
/// </code>
/// <code>
/// [UInt(Minimum = 1000000)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [UInt(Minimum = 2000000, Maximum = 4000000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UIntAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IUInt"/> type.
    /// The default value is 0.
    /// </summary>
    public new uint Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IUInt"/> type.
    /// The default value is 4,294,967,295.
    /// </summary>
    public new uint Maximum { get; set; }
}
