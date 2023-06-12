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
    public void BsonSerializers_Types_Are_Registered_Correctly_Using_IPrimitive_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var expectedTypes = new Type[] { typeof(BirthDate), typeof(IntId), typeof(EightDigits) };

        // Act
        services.AddPrimitively(configure =>
        {
            // Add MongoDB support
            configure.UseMongoDB(builder =>
            {
                // Generate and register BSON serializers for the given source generated Primitive types
                builder.AddBsonSerializerFor<BirthDate>();
                builder.AddBsonSerializerFor<IntId>();
                builder.AddBsonSerializerFor<EightDigits>();
            });
        });

        // Assert
        AssertThatBsonSerializersAreRegistered(expectedTypes);
    }

    [Fact]
    public void BsonSerializers_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();
        var excludedTypes = new Type[] { typeof(BirthDate), typeof(IntId), typeof(EightDigits) };
        var expectedTypes = new PrimitiveRepository().GetTypes().Select(t => t.Type).Where(t => !excludedTypes.Any(e => e == t));

        // Act
        services.AddPrimitively(configure =>
        {
            // Add MongoDB support
            configure.UseMongoDB(builder =>
            {
                // Generate and register BSON serializers for all the types
                // contained in the given source generated Primitive repository
                builder.AddBsonSerializersFor<PrimitiveRepository>();
            });
        });

        // Assert
        AssertThatBsonSerializersAreRegistered(expectedTypes);
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
