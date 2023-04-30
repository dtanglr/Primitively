using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using static Primitively.MongoDb.PrimitivelyBson;

namespace Primitively.MongoDb;

public class PrimitivelyBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IPrimitive
{
    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            return new();
        }

        return Deserialize<TPrimitive>(context);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        Serialize<TPrimitive>(context, value);
    }
}
