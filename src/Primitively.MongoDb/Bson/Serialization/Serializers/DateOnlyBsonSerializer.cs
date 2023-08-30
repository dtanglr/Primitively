using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb.Bson.Serialization.Serializers;

public class DateOnlyBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IPrimitive, IDateOnly
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        _ = DateOnly.TryParse(value.ToString(), out var date);
        context.Writer.WriteDateTime(new DateTime(date.Year, date.Month, date.Day).Ticks);
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // Return default if null
            return new();
        }

        var value = DateOnly.FromDateTime(new DateTime(context.Reader.ReadDateTime()));

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
