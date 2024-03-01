namespace Primitively.MongoDB.Bson.Serialization;

/// <summary>
/// Provides an interface for options that can be used to configure BSON convertible serializers.
/// </summary>
/// <typeparam name="TOptions">The type of the BSON serializer options.</typeparam>
public interface IBsonConvertibleSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : class, IBsonSerializerOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to allow overflow when converting values.
    /// </summary>
    bool AllowOverflow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow truncation when converting values.
    /// </summary>
    bool AllowTruncation { get; set; }
}
