namespace Primitively;

/// <summary>
/// The <see cref="IntAttribute"/> should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IInt"/> type that encapsulates an <see cref="int"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Int attribute to source generate a Primitively <see cref="IInt"/> type.
/// <code>
/// [Int]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Int(Minimum = 0)]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Int(Minimum = 1000, Maximum = 2000)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class IntAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IInt"/> type.
    /// </summary>
    /// <value>
    /// The default value is -2,147,483,648. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new int Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IInt"/> type.
    /// </summary>
    /// <value>
    /// The default value is 2,147,483,647. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new int Maximum { get; set; }
}
