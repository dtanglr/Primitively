namespace Primitively;

/// <summary>
/// The <c>IntegerAttribute</c> is an <see langword="abstract"/> <see langword="class"/>
/// for custom attributes that can be applied to integer types.
/// <para>It inherits from the <see cref="PrimitiveAttribute"/> class and provides properties to set the minimum and maximum values.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public abstract class IntegerAttribute : PrimitiveAttribute
{
    /// <summary>
    /// The minimum value that can be set on the source generated Primitively type.
    /// </summary>
    public decimal Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be set on the source generated Primitively type.
    /// </summary>
    public decimal Maximum { get; set; }
}
