using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.MongoDb;

namespace Primitively.AspNetCore;

public static class PrimitiveMongoDbExtensions
{
    public static IServiceCollection RegisterPrimitiveBsonSerializers(this IServiceCollection services)
    {
        var repositories = services
            .BuildServiceProvider()
            .GetServices<IPrimitiveRepository>();

        if (!repositories.Any())
        {
            return services;
        }

        return services.RegisterPrimitiveBsonSerializers(repositories.ToArray());
    }

    public static IServiceCollection RegisterPrimitiveBsonSerializers(this IServiceCollection services, params IPrimitiveRepository[] repositories)
    {
        if (!repositories.Any())
        {
            return services;
        }

        var types = repositories
            .SelectMany(r => r.GetTypes())
            .Select(p => p.ValueType)
            .Distinct()
            .ToList();

        if (!types.Any())
        {
            return services;
        }

        foreach (var type in types)
        {
            BsonSerializer.TryRegisterSerializer(type, CreateInstance(type));
            BsonSerializer.TryRegisterSerializer(typeof(Nullable<>).MakeGenericType(type), NullableSerializer.Create(CreateInstance(type)));
        }

        return services;
    }

    private static IBsonSerializer CreateInstance(Type primitiveType)
    {
        var serializerType = typeof(PrimitiveSerializer<>).MakeGenericType(primitiveType);
        var instance = Activator.CreateInstance(serializerType) ?? throw new Exception($"Unable to create serializer instance for type: {primitiveType.FullName}");

        return (IBsonSerializer)instance;
    }
}
