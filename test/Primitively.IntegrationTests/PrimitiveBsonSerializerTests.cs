using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Moq;
using Primitively.Configuration;
using Primitively.MongoDB.Bson;
using Primitively.MongoDB.Bson.Serialization;
using Primitively.MongoDB.Bson.Serialization.Options;
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
    public void BsonSerializers_Are_Registered_For_Repository_Types_From_Register_Method()
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

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IByte_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIByteSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIByteSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIByteSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.Byte);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IDateOnly_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIDateOnlySerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIDateOnlySerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIDateOnlySerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.DateOnly);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IGuid_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        var guidRepresentation = GuidRepresentation.CSharpLegacy;
        Func<BsonIGuidSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIGuidSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.GuidRepresentation = guidRepresentation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIGuidSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.Guid);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.GuidRepresentation.Should().Be(guidRepresentation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IInt_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIIntSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIIntSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIIntSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.Int);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_ILong_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonILongSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonILongSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonILongSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.Long);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_ISByte_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonISByteSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonISByteSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonISByteSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.SByte);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IShort_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIShortSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIShortSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIShortSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.Short);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IString_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.Binary;
        Func<BsonIStringSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIStringSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIStringSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.String);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IUInt_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIUIntSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIUIntSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIUIntSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.UInt);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IULong_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIULongSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIULongSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIULongSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.ULong);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }

    [Fact]
    public void Replacement_BsonSerializerOptions_For_IUShort_Serializer_Are_Set_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        var serializerType = typeof(IBsonSerializer);
        var representation = BsonType.String;
        Func<BsonIUShortSerializerOptions, Type, IBsonSerializer> createInstance = (options, primitiveType) => new Mock<IBsonSerializer>().Object;

        // Act
        services.AddPrimitively()
            .AddBson(builder => builder.Configure<BsonIUShortSerializerOptions>(options =>
            {
                options.SerializerType = serializerType;
                options.Representation = representation;
                options.CreateInstance = createInstance;
            }));

        // Assert
        var provider = services.BuildServiceProvider();
        var options = provider.GetService<BsonOptions>();
        options.Should().NotBeNull();

        var serializer = options!.GetSerializerOptions<BsonIUShortSerializerOptions>();
        serializer.Should().NotBeNull();
        serializer.DataType.Should().Be(DataType.UShort);
        serializer.SerializerType.Should().Be(serializerType);
        serializer.Representation.Should().Be(representation);
        serializer.CreateInstance.Should().Be(createInstance);
    }
}
