using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDb;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact]
    public void BsonSerializers_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = new PrimitiveRepository();

        // Act
        services.AddPrimitively(configure =>
        {
            // Add MongoDB support
            configure.UseMongoDB(builder =>
            {
                // Generate and register BSON serializers for all the types
                // contained in the given source generated Primitive repository
                builder.AddBsonSerializersFor(repository);
            });
        });

        // Assert
        AssertThatBsonSerializersAreRegistered(repository.GetTypes().Select(t => t.Type));
    }

    private static void AssertThatBsonSerializersAreRegistered(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            // Bson Serializers
            var nullableType = typeof(Nullable<>).MakeGenericType(type);
            var serializerType = typeof(PrimitiveBsonSerializer<>).MakeGenericType(type);
            var nullableSerializerType = typeof(NullableSerializer<>).MakeGenericType(type);

            // Assert non-nullable serializer created
            BsonSerializer.LookupSerializer(type).Should().BeOfType(serializerType);

            // Assert nullable serializer created
            BsonSerializer.LookupSerializer(nullableType).Should().BeOfType(nullableSerializerType);
        }
    }
}
