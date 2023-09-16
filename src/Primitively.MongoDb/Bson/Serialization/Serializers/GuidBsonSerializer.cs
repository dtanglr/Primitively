using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization.Serializers;

public class GuidBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IGuid
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        context.Writer.WriteString(value.ToString());
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = new Guid(context.Reader.ReadString());

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
