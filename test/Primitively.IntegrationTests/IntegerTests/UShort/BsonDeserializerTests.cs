using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.UShort;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = UShortId.Maximum;
        var expected = (UShortId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new UShortBsonSerializer<UShortId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_With_Exception_When_Bson_Greater_Then_Maximum()
    {
        // Assign
        var number = UShortId.Maximum + 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new UShortBsonSerializer<UShortId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<OverflowException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = UShortId.Minimum;
        var expected = (UShortId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new UShortBsonSerializer<UShortId>();
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
        var number = UShortId.Minimum - 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new UShortBsonSerializer<UShortId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<OverflowException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = UShortId.Maximum;
        var expected = (UShortId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new UShortBsonSerializer<UShortId>());
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
        var number = UShortId.Maximum + 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new UShortBsonSerializer<UShortId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<OverflowException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = UShortId.Minimum;
        var expected = (UShortId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new UShortBsonSerializer<UShortId>());
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
        var number = UShortId.Minimum - 1;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new UShortBsonSerializer<UShortId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Int32);
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = () => serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Throw<OverflowException>();
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (UShortId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new UShortBsonSerializer<UShortId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
    }
}
