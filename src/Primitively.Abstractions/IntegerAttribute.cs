namespace Primitively;

[AttributeUsage(AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public abstract class IntegerAttribute : PrimitiveAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public decimal Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public decimal Maximum { get; set; }
}
