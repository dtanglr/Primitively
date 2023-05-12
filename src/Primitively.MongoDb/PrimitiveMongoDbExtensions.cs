using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.MongoDb;

namespace Primitively.AspNetCore;

public static class PrimitiveMongoDbExtensions
{
    public static IServiceCollection RegisterPrimitiveBsonSerializers(this IServiceCollection services, params IPrimitiveRepository[] repositories)
    {
        if (!repositories.Any())
        {
            return services;
        }

        // Get a list of the source generated Primitively types
        var primitiveTypes = repositories
            .SelectMany(r => r.GetTypes())
            .Select(p => p.Type)
            .ToArray();

        return RegisterPrimitiveBsonSerializers(services, primitiveTypes);
    }

    public static IServiceCollection RegisterPrimitiveBsonSerializers(this IServiceCollection services, params Type[] primitiveTypes)
    {
        if (!primitiveTypes.Any())
        {
            return services;
        }

        foreach (var primitiveType in primitiveTypes.Where(t => t.IsAssignableTo(typeof(IPrimitive))).Distinct())
        {
            // Construct a Primitively serializer of the Primitively type
            var serializerType = typeof(PrimitiveSerializer<>).MakeGenericType(primitiveType);

            // Create a Primitively serializer instance
            var serializerInstance = CreateInstance(serializerType);

            // Register a Serializer for the Primitively type
            BsonSerializer.TryRegisterSerializer(primitiveType, serializerInstance);

            // Construct a nullable version of the Primitively type
            var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);

            // Create a Nullable Primitively serializer instance
            var nullableSerializerInstance = NullableSerializer.Create(serializerInstance);

            // Register a NullableSerializer for a nullable version of the Primitively type
            BsonSerializer.TryRegisterSerializer(nullablePrimitiveType, nullableSerializerInstance);
        }

        return services;
    }

    private static IBsonSerializer CreateInstance(Type serializerType)
    {
        var instance = Activator.CreateInstance(serializerType)
            ?? throw new Exception($"Unable to create serializer instance for type: {serializerType.FullName}");

        return (IBsonSerializer)instance;
    }
}
