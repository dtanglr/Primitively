using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.SByte;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = SByteId.Maximum;
        var expected = (SByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonISByteSerializer<SByteId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_With_Exception_When_Bson_Greater_Then_Maximum()
    {
        // Assign
        var number = SByteId.Maximum + 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonISByteSerializer<SByteId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<FormatException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = SByteId.Minimum;
        var expected = (SByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonISByteSerializer<SByteId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_With_Exception_When_Bson_Less_Then_Minimum()
    {
        // Assign
        var number = SByteId.Minimum - 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonISByteSerializer<SByteId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<FormatException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = SByteId.Maximum;
        var expected = (SByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonISByteSerializer<SByteId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_With_Exception_When_Bson_Greater_Then_Maximum()
    {
        // Assign
        var number = SByteId.Maximum + 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonISByteSerializer<SByteId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<FormatException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = SByteId.Minimum;
        var expected = (SByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonISByteSerializer<SByteId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_With_Exception_When_Bson_Less_Then_Minimum()
    {
        // Assign
        var number = SByteId.Minimum - 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonISByteSerializer<SByteId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<FormatException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (SByteId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonISByteSerializer<SByteId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
    }
}
