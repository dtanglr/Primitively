namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="ISingle"/> type that encapsulates a <see cref="float"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Single attribute to source generate a Primitively <see cref="ISingle"/> type.
/// <code>
/// [Single]
/// public partial record struct Example;
/// 
/// [Single(Minimum = 1.10f)]
/// public partial record struct Example;
/// 
/// [Single(Minimum = -10.10f, Maximum = 10.10f)]
/// public partial record struct Example;
/// 
/// [Single(2, Minimum = -10.10f, Maximum = 10.10f)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class SingleAttribute : NumericAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleAttribute"/> class.
    /// </summary>
    public SingleAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleAttribute"/> class with the specified number of fractional digits.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="ISingle"/> type.</param>
    public SingleAttribute(int digits)
    {
        Digits = digits;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleAttribute"/> class with the specified number of fractional digits and rounding mode.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="ISingle"/> type.</param>
    /// <param name="mode">The rounding specification for how to round value of the source generated Primitively <see cref="ISingle"/> type if it is midway between two other numbers.</param>
    public SingleAttribute(int digits, MidpointRounding mode)
    {
        Digits = digits;
        Mode = mode;
    }

    /// <summary>
    /// Gets the number of fractional digits in the value of the source generated Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <remarks>
    /// Valid values are: -1 to 6.
    /// Values above 6 will default to: -1.
    /// Values below -1 with default to: -1.
    /// A value of -1 will result in: no rounding.
    /// </remarks>
    public int Digits { get; }

    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="ISingle"/> type.
    /// </summary>
    /// <value>
    /// The default value is float.MaxValue. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new float Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="ISingle"/> type.
    /// </summary>
    /// <value>
    /// The default value is float.MinValue. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new float Maximum { get; set; }

    /// <summary>
    /// Gets the rounding specification for how to round value of the source generated Primitively <see cref="ISingle"/> type 
    /// if it is midway between two other numbers.
    /// </summary>
    /// <remarks>
    /// If the value of <see cref="Digits"/> is -1, this property will have no effect.
    /// </remarks>
    public MidpointRounding Mode { get; }
}
