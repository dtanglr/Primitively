namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates a Unsigned 8-bit integer primitive value
/// with a default range of: 0 to 255
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class ByteAttribute : Attribute, IIntegerAttribute<byte>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public byte Minimum { get; set; } = byte.MinValue;

    /// <inheritdoc/>
    public byte Maximum { get; set; } = byte.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
