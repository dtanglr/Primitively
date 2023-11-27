namespace Primitively.MongoDB.Bson.Serialization.Options;

public interface IRepresentationConverterOptions
{
    bool AllowOverflow { get; set; }
    bool AllowTruncation { get; set; }
}
