namespace Primitively;

/// <summary>
/// The <c>IntAttribute</c> class can be used on a <see langword="partial record struct"/>
/// to source generate a Primitively <see cref="IInt"/> type that encapsulates an <see cref="int"/> value.
/// </summary>
/// <example>
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
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class IntAttribute : IntegerAttribute
{
    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IInt"/> type.
    /// The default value is -2,147,483,648.
    /// </summary>
    public new int Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IInt"/> type.
    /// The default value is 2,147,483,647.
    /// </summary>
    public new int Maximum { get; set; }
}
