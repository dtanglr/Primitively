using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDB.Bson.Serialization.Serializers;

public class SByteBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, ISByte
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        context.Writer.WriteInt32(value.Value);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = Convert.ToSByte(context.Reader.ReadInt32());

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
