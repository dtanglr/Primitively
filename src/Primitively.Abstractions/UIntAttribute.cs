namespace Primitively;

/// <summary>
/// Make a readonly record struct that encapsulates an Unsigned 32-bit integer primitive value
/// with a default range of: 0 to 4,294,967,295
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
public sealed class UIntAttribute : Attribute, IIntegerAttribute<uint>
{
    /// <inheritdoc/>
    public bool ImplementIValidatableObject { get; set; }

    /// <inheritdoc/>
    public uint Minimum { get; set; } = uint.MinValue;

    /// <inheritdoc/>
    public uint Maximum { get; set; } = uint.MaxValue;

    object IIntegerAttribute.Minimum { get => Minimum; }

    object IIntegerAttribute.Maximum { get => Maximum; }
}
