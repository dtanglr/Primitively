namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 32-bit integer primitive value
/// with a default range of: 0 to 4,294,967,295
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UIntAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new uint Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new uint Maximum { get; set; }
}
