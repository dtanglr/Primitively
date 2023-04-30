using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using static Primitively.MongoDb.PrimitivelyBson;

namespace Primitively.MongoDb;

public class PrimitivelyNullableBsonSerializer<TPrimitive> : SerializerBase<TPrimitive?>
    where TPrimitive : struct, IPrimitive
{
    public override TPrimitive? Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            return null;
        }

        return Deserialize<TPrimitive>(context);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive? value)
    {
        if (value == null)
        {
            context.Writer.WriteNull();

            return;
        }

        Serialize<TPrimitive>(context, (TPrimitive)value);
    }
}
