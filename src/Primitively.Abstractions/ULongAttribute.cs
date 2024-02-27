namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 64-bit integer primitive value
/// with a default range of: 0 to 18,446,744,073,709,551,615
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ULongAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new ulong Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new ulong Maximum { get; set; }
}
