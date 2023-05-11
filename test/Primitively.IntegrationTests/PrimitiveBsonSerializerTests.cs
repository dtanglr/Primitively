using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.AspNetCore;
using Primitively.MongoDb;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    // Use reflection to get Primitively source generated types
    private static readonly IEnumerable<Type> _types = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.IsValueType && t.IsAssignableTo(typeof(IPrimitive)));

    [Fact]
    public void BsonSerializers_Types_Are_Registered_Correctly_Using_IPrimitiveRepository_Instances_In_Params()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.RegisterPrimitiveBsonSerializers(new PrimitiveRepository());

        // Assert
        AssertThatBsonSerializersAreRegistered();
    }

    private static void AssertThatBsonSerializersAreRegistered()
    {
        foreach (var type in _types)
        {
            // Bson Serializers
            var nullableType = typeof(Nullable<>).MakeGenericType(type);
            var serializerType = typeof(PrimitiveSerializer<>).MakeGenericType(type);
            var nullableSerializerType = typeof(NullableSerializer<>).MakeGenericType(type);

            // Assert non-nullable serializer created
            BsonSerializer.LookupSerializer(type).Should().BeOfType(serializerType);

            // Assert nullable serializer created
            BsonSerializer.LookupSerializer(nullableType).Should().BeOfType(nullableSerializerType);
        }
    }
}
