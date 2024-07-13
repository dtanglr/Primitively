namespace Primitively;

/// <summary>
/// This attribute should be used on a <c>partial record struct</c> to source generate
/// a Primitively <see cref="IDecimal"/> type that encapsulates a <see cref="decimal"/> value.
/// </summary>
/// <example>
/// These examples show how to use the Decimal attribute to source generate a Primitively <see cref="IDecimal"/> type.
/// <code>
/// [Decimal]
/// public partial record struct Example;
/// </code>
/// <code>
/// [Decimal(3)]
/// public partial record struct Example;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class DecimalAttribute : NumericAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalAttribute"/> class.
    /// </summary>
    public DecimalAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalAttribute"/> class with the specified number of fractional digits.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="IDecimal"/> type.</param>
    public DecimalAttribute(int digits)
    {
        Digits = digits;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalAttribute"/> class with the specified number of fractional digits and rounding mode.
    /// </summary>
    /// <param name="digits">The number of fractional digits in the value of the source generated Primitively <see cref="IDecimal"/> type.</param>
    /// <param name="mode">The rounding specification for how to round value of the source generated Primitively <see cref="IDecimal"/> type if it is midway between two other numbers.</param>
    public DecimalAttribute(int digits, MidpointRounding mode)
    {
        Digits = digits;
        Mode = mode;
    }

    /// <summary>
    /// Gets the number of fractional digits in the value of the source generated Primitively <see cref="IDecimal"/> type.
    /// </summary>
    /// <remarks>
    /// Valid values are: -1 to 28.
    /// Values above 28 will default to: -1.
    /// Values below -1 will default to: -1.
    /// A value of -1 will result in: no rounding.
    /// </remarks>
    public int Digits { get; }

    /// <summary>
    /// Gets the rounding specification for how to round value of the source generated Primitively <see cref="IDecimal"/> type 
    /// if it is midway between two other numbers.
    /// </summary>
    /// <remarks>
    /// If the value of <see cref="Digits"/> is -1, this property will have no effect.
    /// </remarks>
    public MidpointRounding Mode { get; }
}
