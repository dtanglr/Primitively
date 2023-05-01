using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Moq;
using Primitively.MongoDb;
using Xunit;

namespace Primitively.IntegrationTests;

public class PrimitiveBsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = ByteId.Maximum;
        var expected = (ByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveSerializer<ByteId>();
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = ByteId.Minimum;
        var expected = (ByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveSerializer<ByteId>();
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = new ByteId();
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveSerializer<ByteId>();
        bsonReader.SetupGet(r => r.CurrentBsonType).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = ByteId.Maximum;
        var expected = (ByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new NullablePrimitiveSerializer<ByteId>();
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = ByteId.Minimum;
        var expected = (ByteId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new NullablePrimitiveSerializer<ByteId>();
        bsonReader.Setup(r => r.ReadInt32()).Returns(number);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Once);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (ByteId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new NullablePrimitiveSerializer<ByteId>();
        bsonReader.SetupGet(r => r.CurrentBsonType).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }
}
