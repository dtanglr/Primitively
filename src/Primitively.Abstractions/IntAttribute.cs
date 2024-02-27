namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 32-bit integer primitive value
/// with a default range of: -2,147,483,648 to 2,147,483,647
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class IntAttribute : IntegerAttribute
{
    /// <summary>
    /// The minimum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The minimum value</value>
    public new int Minimum { get; set; }

    /// <summary>
    /// The maximum value that can be assigned to the Primitively type
    /// </summary>
    /// <value>The maximum value</value>
    public new int Maximum { get; set; }
}
