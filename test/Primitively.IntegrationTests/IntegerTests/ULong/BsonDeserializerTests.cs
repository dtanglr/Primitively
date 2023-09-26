using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDb.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.ULong;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = ULongId.Maximum;
        var expected = (ULongId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new ULongBsonSerializer<ULongId>();
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(new Decimal128(expected));

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = ULongId.Minimum;
        var expected = (ULongId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new ULongBsonSerializer<ULongId>();
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(new Decimal128(expected));

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = new ULongId();
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new ULongBsonSerializer<ULongId>();
        bsonReader.SetupGet(r => r.CurrentBsonType).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Never);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Maximum()
    {
        // Assign
        var number = ULongId.Maximum;
        var expected = (ULongId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new ULongBsonSerializer<ULongId>());
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(new Decimal128(expected));

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Minimum()
    {
        // Assign
        var number = ULongId.Minimum;
        var expected = (ULongId)number;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new ULongBsonSerializer<ULongId>());
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(new Decimal128(expected));

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (ULongId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new ULongBsonSerializer<ULongId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.GetCurrentBsonType(), Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Never);
    }
}
