namespace Primitively.MongoDb;

public interface IPrimitiveBsonSerializerBuilder
{
    IPrimitiveBsonSerializerBuilder AddBsonSerializerFor<T>() where T : struct, IPrimitive;
    IPrimitiveBsonSerializerBuilder AddBsonSerializersFor<T>() where T : class, IPrimitiveRepository, new();
}
