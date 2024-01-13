namespace Primitively.MongoDB.Bson.Serialization;

public interface IBsonConvertibleSerializerOptions<TOptions> : IBsonSerializerOptions<TOptions>
    where TOptions : class, IBsonSerializerOptions
{
    bool AllowOverflow { get; set; }
    bool AllowTruncation { get; set; }
}
