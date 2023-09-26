using Primitively.MongoDb.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization;

public class BsonSerializerOptions
{
    public Type ByteBsonSerializer { get; set; } = typeof(ByteBsonSerializer<>);
    public Type DateOnlyBsonSerializer { get; set; } = typeof(DateOnlyBsonSerializer<>);
    public Type GuidBsonSerializer { get; set; } = typeof(GuidBsonSerializer<>);
    public Type IntBsonSerializer { get; set; } = typeof(IntBsonSerializer<>);
    public Type LongBsonSerializer { get; set; } = typeof(LongBsonSerializer<>);
    public Type SByteBsonSerializer { get; set; } = typeof(SByteBsonSerializer<>);
    public Type ShortBsonSerializer { get; set; } = typeof(ShortBsonSerializer<>);
    public Type StringBsonSerializer { get; set; } = typeof(StringBsonSerializer<>);
    public Type UIntBsonSerializer { get; set; } = typeof(UIntBsonSerializer<>);
    public Type ULongBsonSerializer { get; set; } = typeof(ULongBsonSerializer<>);
    public Type UShortBsonSerializer { get; set; } = typeof(UShortBsonSerializer<>);
}
