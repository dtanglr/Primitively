using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDb;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.Int;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = IntId.Maximum;
        var expected = (IntId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveBsonSerializer<IntId>();
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
        var number = IntId.Minimum;
        var expected = (IntId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveBsonSerializer<IntId>();
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
        var expected = new IntId();
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new PrimitiveBsonSerializer<IntId>();
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
        var number = IntId.Maximum;
        var expected = (IntId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new PrimitiveBsonSerializer<IntId>());
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
        var number = IntId.Minimum;
        var expected = (IntId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new PrimitiveBsonSerializer<IntId>());
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
        var expected = (IntId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new PrimitiveBsonSerializer<IntId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.GetCurrentBsonType(), Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
    }
}
