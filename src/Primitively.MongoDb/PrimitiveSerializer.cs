using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

public class PrimitiveSerializer<TPrimitive> : SerializerBase<TPrimitive>
    where TPrimitive : struct, IPrimitive
{
    public override TPrimitive Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();

            // TODO: Revisit this and decide whether to throw an error instead, rather than return a
            // struct in it's default state. 
            return new();
        }

        var type = typeof(TPrimitive);

        object value = type switch
        {
            _ when type.IsAssignableTo(typeof(IByte)) => Convert.ToByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(ISByte)) => Convert.ToSByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IShort)) => Convert.ToInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IUShort)) => Convert.ToUInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IInt)) => context.Reader.ReadInt32(),
            _ when type.IsAssignableTo(typeof(IUInt)) => Convert.ToUInt32(context.Reader.ReadInt64()),
            _ when type.IsAssignableTo(typeof(ILong)) => context.Reader.ReadInt64(),
            _ when type.IsAssignableTo(typeof(IULong)) => Convert.ToUInt64(context.Reader.ReadString()),
            _ when type.IsAssignableTo(typeof(IDateOnly)) => DateOnly.Parse(context.Reader.ReadString()),
            _ when type.IsAssignableTo(typeof(IGuid)) => Guid.Parse(context.Reader.ReadString()),
            _ when type.IsAssignableTo(typeof(IString)) => context.Reader.ReadString(),
            _ => new NotImplementedException()
        };

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TPrimitive value)
    {
        switch (value)
        {
            case ILong:
            case IUInt:
                {
                    _ = long.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt64(num);
                    break;
                }

            case IULong: // Store as string because an unsigned long exceeds Mongo's Int64 maximum
                {
                    context.Writer.WriteString(value.ToString());
                    break;
                }

            case IInteger: // Store all other numbers as int32 e.g. IByte, ISByte, IInt, IUInt
                {
                    _ = int.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt32(num);
                    break;
                }

            case IDateOnly:
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
}
