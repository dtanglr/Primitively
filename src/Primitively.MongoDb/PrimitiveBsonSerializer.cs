using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

public class PrimitiveBsonSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IPrimitive
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        switch (value)
        {
            // Store as decimal128 because an unsigned long can exceed the Mongo int64 maximum
            case IULong:
                {
                    _ = ulong.TryParse(value.ToString(), out var num);
                    context.Writer.WriteDecimal128(new Decimal128(num));
                    break;
                }

            // Store as int64 (uint numbers can exceed the int32 maximium, so store them safely as int64 instead)
            case ILong:
            case IUInt:
                {
                    _ = long.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt64(num);
                    break;
                }

            // Store as int32 (bytes and sbytes are stored as int32 because Mongo doesn't support smaller number types AFAIK)
            case IByte:
            case ISByte:
            case IUShort:
            case IShort:
            case IInt:
                {
                    _ = int.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt32(num);
                    break;
                }

            // Store as DateTime
            case IDateOnly:
                {
                    _ = DateOnly.TryParse(value.ToString(), out var date);
                    context.Writer.WriteDateTime(new DateTime(date.Year, date.Month, date.Day).Ticks);
                    break;
                }

            // Store as string
            case IGuid:
            case IString:
                {
                    context.Writer.WriteString(value.ToString());
                    break;
                }

            default:
                throw new NotImplementedException();
        }
    }

    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // This deserialize is for a non-nullable Primitively type; so if the DB has a null value stored for the value,
            // instantiate a default instance of the Primitively struct and return that.
            return new();
        }

        var type = typeof(TPrimitive);

        object value = type switch
        {
            _ when type.IsAssignableTo(typeof(IULong)) => Decimal128.ToUInt64(context.Reader.ReadDecimal128()),
            _ when type.IsAssignableTo(typeof(ILong)) => context.Reader.ReadInt64(),
            _ when type.IsAssignableTo(typeof(IUInt)) => Convert.ToUInt32(context.Reader.ReadInt64()),
            _ when type.IsAssignableTo(typeof(IInt)) => context.Reader.ReadInt32(),
            _ when type.IsAssignableTo(typeof(IUShort)) => Convert.ToUInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IShort)) => Convert.ToInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IByte)) => Convert.ToByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(ISByte)) => Convert.ToSByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IDateOnly)) => DateOnly.FromDateTime(new DateTime(context.Reader.ReadDateTime())),
            _ when type.IsAssignableTo(typeof(IGuid)) => new Guid(context.Reader.ReadString()),
            _ when type.IsAssignableTo(typeof(IString)) => context.Reader.ReadString(),
            _ => new NotImplementedException()
        };

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }
}
