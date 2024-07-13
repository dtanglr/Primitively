using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Decimal;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Default_Bson_String_Correctly()
    {
        // Assign
        var example = DecimalId.Example;
        var expected = (DecimalId)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonIDecimalSerializer<DecimalId>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.String);
        bsonReader.Setup(r => r.ReadString()).Returns(expected);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadString(), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Decimal128_Correctly()
    {
        // Assign
        var example = DecimalId.Example;
        var expected = (DecimalId)example;
        var expectedAsDecimal128 = new Decimal128(expected);
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonIDecimalSerializer<DecimalId>(BsonType.Decimal128);
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Decimal128);
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(expectedAsDecimal128);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Default_Bson_String_Correctly()
    {
        // Assign
        var example = DecimalId.Example;
        var expected = (DecimalId)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.String);
        bsonReader.Setup(r => r.ReadString()).Returns(expected);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadString(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Decimal128_Correctly()
    {
        // Assign
        var example = DecimalId.Example;
        var expected = (DecimalId)example;
        var expectedAsDecimal128 = new Decimal128(expected);
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalId>(BsonType.Decimal128));
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Decimal128);
        bsonReader.Setup(r => r.ReadDecimal128()).Returns(expectedAsDecimal128);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (DecimalId?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalId>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadDecimal128(), Times.Never);
    }
}
