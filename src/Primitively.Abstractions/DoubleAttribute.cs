namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IDouble"/> type that encapsulates a <see cref="double"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Double attribute to source generate a Primitively <see cref="IDouble"/> type.
/// <code>
/// [Double]
/// public partial record struct Example;
/// 
/// [Double(Minimum = 1.10d)]
/// public partial record struct Example;
/// 
/// [Double(Minimum = -10.10d, Maximum = 10.10d)]
/// public partial record struct Example;
/// 
/// [Double(2, Minimum = -10.10d, Maximum = 10.10d)]
/// public partial record struct Example;
/// </code>
/// </example>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class DoubleAttribute : NumericAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleAttribute"/> class.
    /// </summary>
    public DoubleAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleAttribute"/> class with the specified number of fractional digits.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="IDouble"/> type.</param>
    public DoubleAttribute(int digits)
    {
        Digits = digits;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleAttribute"/> class with the specified number of fractional digits and rounding mode.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="IDouble"/> type.</param>
    /// <param name="mode">The rounding specification for how to round value of the source generated Primitively <see cref="IDouble"/> type if it is midway between two other numbers.</param>
    public DoubleAttribute(int digits, MidpointRounding mode)
    {
        Digits = digits;
        Mode = mode;
    }

    /// <summary>
    /// Gets the number of fractional digits in the value of the source generated Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <remarks>
    /// Valid values are: -1 to 15.
    /// Values above 15 will default to: -1.
    /// Values below -1 will default to: -1.
    /// A value of -1 will result in: no rounding.
    /// </remarks>
    public int Digits { get; }

    /// <summary>
    /// Gets or sets the maximum value supported by the source generated Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <value>
    /// The default value is double.MaxValue. An assigned value should not be less than the <see cref="Minimum"/> value.
    /// </value>
    public new double Maximum { get; set; }

    /// <summary>
    /// Gets or sets the minimum value supported by the source generated Primitively <see cref="IDouble"/> type.
    /// </summary>
    /// <value>
    /// The default value is double.MinValue. An assigned value should not be greater than the <see cref="Maximum"/> value.
    /// </value>
    public new double Minimum { get; set; }

    /// <summary>
    /// Gets the rounding specification for how to round value of the source generated Primitively <see cref="IDouble"/> type 
    /// if it is midway between two other numbers.
    /// </summary>
    /// <remarks>
    /// If the value of <see cref="Digits"/> is -1, this property will have no effect.
    /// </remarks>
    public MidpointRounding Mode { get; }
}
