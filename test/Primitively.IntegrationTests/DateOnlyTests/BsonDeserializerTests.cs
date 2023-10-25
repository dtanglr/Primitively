using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.DateOnlyTests;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = BirthDate.Example;
        var expected = (BirthDate)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new DateOnlyBsonSerializer<BirthDate>();
        bsonReader.Setup(r => r.ReadDateTime()).Returns(DateTime.Parse(expected).Ticks);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDateTime(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = new BirthDate();
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new DateOnlyBsonSerializer<BirthDate>();
        bsonReader.SetupGet(r => r.CurrentBsonType).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.CurrentBsonType, Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDateTime(), Times.Never);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = BirthDate.Example;
        var expected = (BirthDate)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new DateOnlyBsonSerializer<BirthDate>());
        bsonReader.Setup(r => r.ReadDateTime()).Returns(DateTime.Parse(expected).Ticks);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDateTime(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (BirthDate?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new DateOnlyBsonSerializer<BirthDate>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.GetCurrentBsonType(), Times.Once);
        bsonReader.Verify(r => r.ReadInt32(), Times.Never);
        bsonReader.Verify(r => r.ReadInt64(), Times.Never);
        bsonReader.Verify(r => r.ReadString(), Times.Never);
        bsonReader.Verify(r => r.ReadDateTime(), Times.Never);
    }
}
