namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 16-bit integer primitive value
/// with a default range of: 0 to 65,535
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UShortAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new ushort Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new ushort Maximum { get; set; }
}
