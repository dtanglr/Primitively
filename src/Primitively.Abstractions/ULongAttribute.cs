namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 64-bit integer primitive value
/// with a default range of: 0 to 18,446,744,073,709,551,615
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ULongAttribute : Attribute, IIntegerAttribute<ulong>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public ulong Minimum { get; set; } = ulong.MinValue;

    /// <inheritdoc/>
    public ulong Maximum { get; set; } = ulong.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
