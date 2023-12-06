using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization.Options;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact]
    public void BsonSerializers_Are_Registered_Using_Types_Sourced_From_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();

        // Act
        services.AddPrimitively(options => options.Register(repository))
            .AddBson();

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitive in primitives)
            {
                var serializer = BsonSerializerOptionsCache.Get(primitive.DataType);
                AssertThatSerializerShouldBeRegistered(primitive.Type, serializer.SerializerType, true);
            }
        }
    }

    [Fact]
    public void BsonSerializers_Are_Registered_Using_Types_Sourced_From_Registry_v2()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();

        // Act
        services.AddPrimitively(options => options.Register(repository))
            .AddBson(o => o.RegisterSerializersForEachTypeInRegistry = true);

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitive in primitives)
            {
                var serializer = BsonSerializerOptionsCache.Get(primitive.DataType);
                AssertThatSerializerShouldBeRegistered(primitive.Type, serializer.SerializerType, true);
            }
        }
    }

    [Fact]
    public void BsonSerializers_Are_Not_Registered_Using_Types_Sourced_From_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();

        // Act
        services.AddPrimitively(options => options.Register(repository))
            .AddBson(o => o.RegisterSerializersForEachTypeInRegistry = false); // Set to false to override the default value of true

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitive in primitives)
            {
                var serializer = BsonSerializerOptionsCache.Get(primitive.DataType);
                AssertThatSerializerShouldBeRegistered(primitive.Type, serializer.SerializerType, false);
            }
        }
    }

    [Fact]
    public void BsonSerializers_Are_Not_Registered_When_No_Types_In_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();

        // Act
        services.AddPrimitively()
            .AddBson();

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitive in primitives)
            {
                var serializer = BsonSerializerOptionsCache.Get(primitive.DataType);
                AssertThatSerializerShouldBeRegistered(primitive.Type, serializer.SerializerType, false);
            }
        }
    }

    private static void AssertThatSerializerShouldBeRegistered(Type primitiveType, Type serializerType, bool shouldBeRegistered)
    {
        // Assert non-nullable serializer created
        var serializer = BsonSerializer.LookupSerializer(primitiveType);

        // Assert nullable serializer created
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);
        var nullableSerializer = BsonSerializer.LookupSerializer(nullablePrimitiveType) as INullableSerializer;
        nullableSerializer.Should().NotBeNull();

        // Assert
        if (shouldBeRegistered)
        {
            serializer.Should().BeOfType(serializerType);
            nullableSerializer!.ValueSerializer.Should().BeOfType(serializerType);
        }
        else
        {
            serializer.Should().NotBeOfType(serializerType);
            nullableSerializer!.ValueSerializer.Should().NotBeOfType(serializerType);
        }
    }
}
