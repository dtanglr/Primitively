using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Moq;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonSerializerTests
{
    [Fact]
    public void BsonSerializers_Are_Registered_For_Types_Sourced_From_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();
        var manager = new Mock<IBsonSerializerManager>();
        manager.Setup(m => m.TryRegisterSerializer(It.IsAny<Type>(), It.IsAny<IBsonSerializer>())).Returns(true);

        // Override the default Bson manager with the mocked version to facilitate unit testing
        // Nb. the default manager acts as a wrapper around the Bson driver static type register,
        // the mocked version facilitates multiple unit tests against the register.
        services.AddSingleton(typeof(IBsonSerializerManager), manager.Object);

        // Act
        services.AddPrimitively(options => options.Register(repository))
            .AddBson();

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitiveInfo in primitives)
            {
                // Non-nullable serializer version
                var primitiveType = primitiveInfo.Type;
                manager.Verify(m => m.TryRegisterSerializer(primitiveType, It.IsAny<IBsonSerializer>()), Times.Once);

                // Nullable serializer version
                var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);
                manager.Verify(m => m.TryRegisterSerializer(nullablePrimitiveType, It.IsAny<IBsonSerializer>()), Times.Once);
            }
        }
    }

    [Fact]
    public void BsonSerializers_Are_Not_Registered_For_Types_Sourced_From_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();
        var manager = new Mock<IBsonSerializerManager>();
        manager.Setup(m => m.TryRegisterSerializer(It.IsAny<Type>(), It.IsAny<IBsonSerializer>())).Returns(true);

        // Override the default Bson manager with the mocked version to facilitate unit testing
        services.AddSingleton(typeof(IBsonSerializerManager), manager.Object);

        // Act
        services.AddPrimitively(options => options.Register(repository))
            .AddBson(o => o.RegisterSerializersForEachTypeInRegistry = false); // Set to false to override the default value of true

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitiveInfo in primitives)
            {
                // Non-nullable serializer version
                var primitiveType = primitiveInfo.Type;
                manager.Verify(m => m.TryRegisterSerializer(primitiveType, It.IsAny<IBsonSerializer>()), Times.Never);

                // Nullable serializer version
                var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);
                manager.Verify(m => m.TryRegisterSerializer(nullablePrimitiveType, It.IsAny<IBsonSerializer>()), Times.Never);
            }
        }
    }

    [Fact]
    public void BsonSerializers_Are_Registered_For_Repository_Types_Sourced_From_Registry()
    {
        // Arrange
        var services = new ServiceCollection();
        var repository = PrimitiveLibrary.Respository;
        var primitives = repository.GetTypes();
        var manager = new Mock<IBsonSerializerManager>();
        manager.Setup(m => m.TryRegisterSerializer(It.IsAny<Type>(), It.IsAny<IBsonSerializer>())).Returns(true);

        // Override the default Bson manager with the mocked version to facilitate unit testing
        services.AddSingleton(typeof(IBsonSerializerManager), manager.Object);

        // Act
        services.AddPrimitively()
            .AddBson(options => options.Register(repository));

        // Assert
        using (new AssertionScope())
        {
            foreach (var primitiveInfo in primitives)
            {
                // Non-nullable serializer version
                var primitiveType = primitiveInfo.Type;
                manager.Verify(m => m.TryRegisterSerializer(primitiveType, It.IsAny<IBsonSerializer>()), Times.Once);

                // Nullable serializer version
                var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);
                manager.Verify(m => m.TryRegisterSerializer(nullablePrimitiveType, It.IsAny<IBsonSerializer>()), Times.Once);
            }
        }
    }

    [Fact]
    public void BsonSerializer_Is_Registered_For_Type_Sourced_From_Register_Method()
    {
        // Arrange
        var services = new ServiceCollection();
        var manager = new Mock<IBsonSerializerManager>();
        manager.Setup(m => m.TryRegisterSerializer(It.IsAny<Type>(), It.IsAny<IBsonSerializer>())).Returns(true);

        // Override the default Bson manager with the mocked version to facilitate unit testing
        services.AddSingleton(typeof(IBsonSerializerManager), manager.Object);

        // Act
        services.AddPrimitively()
            .AddBson(options => options.Register<BirthDate>());

        // Assert
        // Non-nullable serializer version
        manager.Verify(m => m.TryRegisterSerializer(typeof(BirthDate), It.IsAny<IBsonSerializer>()), Times.Once);

        // Nullable serializer version
        var nullablePrimitiveType = typeof(Nullable<>).MakeGenericType(typeof(BirthDate));
        manager.Verify(m => m.TryRegisterSerializer(nullablePrimitiveType, It.IsAny<IBsonSerializer>()), Times.Once);
    }
}
