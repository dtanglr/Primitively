namespace Primitively;

/// <summary>
/// This class can be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IUInt"/> type that encapsulates an <see cref="uint"/> value.
/// </summary>
/// <example>
/// These examples show how to use the UInt attribute to source generate a Primitively <see cref="IUInt"/> type.
/// <code>
/// [UInt]
/// public partial record struct Example;
/// 
/// [UInt(Minimum = 1000000)]
/// public partial record struct Example;
/// 
/// [UInt(Minimum = 2000000, Maximum = 4000000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UIntAttribute : NumericAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IUInt"/> type.
    /// </summary>
    /// <value>
    /// The default value is 0. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new uint Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IUInt"/> type.
    /// </summary>
    /// <value>
    /// The default value is 4,294,967,295. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new uint Maximum { get; set; }
}
