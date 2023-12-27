namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonConvertibleSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : IBsonSerializerOptions
{
    bool AllowOverflow { get; internal set; }
    bool AllowTruncation { get; internal set; }
}
