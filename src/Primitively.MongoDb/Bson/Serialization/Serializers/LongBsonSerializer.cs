using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization.Serializers;

public class LongBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, ILong
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        _ = long.TryParse(value.ToString(), out var num);
        context.Writer.WriteInt64(num);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = context.Reader.ReadInt64();

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
