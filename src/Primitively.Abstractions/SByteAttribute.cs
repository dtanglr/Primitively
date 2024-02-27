namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 8-bit integer primitive value
/// with a default range of: -128 to 127
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class SByteAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new sbyte Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new sbyte Maximum { get; set; }
}
