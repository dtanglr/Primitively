using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Primitively.MongoDb;

/// <summary>
/// Service collection extensions to register Mongo BSON serializers for Primitively source generated types
/// </summary>
public static class PrimitiveMongoDbExtensions
{
    /// <summary>
    /// Automatically register Mongo BSON serilizers for all the types stored in each Primitive Repository provided
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="repositories">One or more source generated Primitive Repository classes</param>
    /// <returns>Services collection</returns>
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

    /// <summary>
    /// Automatically register Mongo BSON serilizers for all the IPrimitive types provided
    /// </summary>
    /// <param name="services">Services Collection</param>
    /// <param name="primitiveTypes">One or more IPrimitive types</param>
    /// <returns>Services collection</returns>
    public static IServiceCollection RegisterPrimitiveBsonSerializers(this IServiceCollection services, params Type[] primitiveTypes)
    {
        if (!primitiveTypes.Any())
        {
            return services;
        }

        // Generate and instance of a serializer and nullable serializer for each Primitively type
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
