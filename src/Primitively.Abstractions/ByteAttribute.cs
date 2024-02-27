namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Unsigned 8-bit integer primitive value
/// with a default range of: 0 to 255
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ByteAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new byte Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new byte Maximum { get; set; }
}
