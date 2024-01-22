namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Signed 32-bit integer primitive value
/// with a default range of: -2,147,483,648 to 2,147,483,647
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class IntAttribute : Attribute, IIntegerAttribute<int>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public int Minimum { get; set; } = int.MinValue;

    /// <inheritdoc/>
    public int Maximum { get; set; } = int.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
