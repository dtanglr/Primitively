namespace Primitively.MongoDb;

public interface IPrimitiveBsonSerializerBuilder
{
    IPrimitiveBsonSerializerBuilder AddBsonSerializerFor(Type primitiveType);
    IPrimitiveBsonSerializerBuilder AddBsonSerializersFor(IPrimitiveRepository primitiveRepository);
}
