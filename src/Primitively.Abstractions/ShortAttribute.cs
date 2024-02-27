namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 16-bit integer primitive value
/// with a default range of: -32,768 to 32,767
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ShortAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new short Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new short Maximum { get; set; }
}
