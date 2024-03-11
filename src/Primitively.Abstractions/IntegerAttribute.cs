namespace Primitively;

/// <summary>
/// This is an abstract class for custom attributes that can be applied to integer types.
/// It inherits from the <see cref="PrimitiveAttribute"/> class and provides properties to set the minimum and maximum values.
/// </summary>
/// <remarks>
/// The generated Primitively type will enforce the specified minimum and maximum value constraints.
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public abstract class IntegerAttribute : PrimitiveAttribute
{
    /// <summary>
    /// Gets or sets the minimum value that can be set on the source generated Primitively type.
    /// </summary>
    public decimal Minimum { get; set; }

    /// <summary>
    /// Gets or sets the maximum value that can be set on the source generated Primitively type.
    /// </summary>
    public decimal Maximum { get; set; }
}
