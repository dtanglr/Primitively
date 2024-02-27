namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 64-bit integer primitive value
/// with a default range of: -9,223,372,036,854,775,807 to 9,223,372,036,854,775,807
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class LongAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new long Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new long Maximum { get; set; }
}
