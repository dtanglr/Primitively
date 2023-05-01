using MongoDB.Bson.Serialization;

namespace Primitively.MongoDb;

public interface IPrimitiveSerializer<TPrimitive> where TPrimitive : struct, IPrimitive
{
    public TPrimitive Deserialize(BsonDeserializationContext context)
    {
        var type = typeof(TPrimitive);

        object value = type switch
        {
            _ when type.IsAssignableTo(typeof(IByte)) => Convert.ToByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(ISByte)) => Convert.ToSByte(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IShort)) => Convert.ToInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IUShort)) => Convert.ToUInt16(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(IInt)) => context.Reader.ReadInt32(),
            _ when type.IsAssignableTo(typeof(IUInt)) => Convert.ToUInt32(context.Reader.ReadInt32()),
            _ when type.IsAssignableTo(typeof(ILong)) => context.Reader.ReadInt64(),
            _ when type.IsAssignableTo(typeof(IULong)) => Convert.ToUInt64(context.Reader.ReadInt64()),
            _ when type.IsAssignableTo(typeof(IDateOnly)) => DateOnly.FromDateTime(Convert.ToDateTime(context.Reader.ReadDateTime())),
            _ when type.IsAssignableTo(typeof(IGuid)) => Guid.Parse(context.Reader.ReadString()),
            _ when type.IsAssignableTo(typeof(IString)) => context.Reader.ReadString(),
            _ => new NotImplementedException()
        };

        return (TPrimitive)Activator.CreateInstance(typeof(TPrimitive), value)!;
    }

    public void Serialize(BsonSerializationContext context, TPrimitive value)
    {
        switch (value)
        {
            case ILong:
            case IULong:
                {
                    _ = long.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt64(num);
                    break;
                }

            case IInteger: // Store all other numbers as int32 e.g. IByte, ISByte, IInt, IUInt
                {
                    _ = int.TryParse(value.ToString(), out var num);
                    context.Writer.WriteInt32(num);
                    break;
                }

            case IDateOnly: // Store DataOnly as ticks
                {
                    _ = DateTime.TryParse(value.ToString(), out var dt);
                    context.Writer.WriteDateTime(dt.Ticks);
                    break;
                }

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
