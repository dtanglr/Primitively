using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Decimal;

public class BsonSerializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Serialises_To_Default_Bson_String_Correctly()
    {
        // Assign
        var example = DecimalWith2Digits.Example;
        var expected = (DecimalWith2Digits)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new BsonIDecimalSerializer<DecimalWith2Digits>();
        bsonWriter.Setup(r => r.WriteString(It.IsAny<string>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(expected), Times.Once);
    }

    [Fact]
    public void Non_Nullable_Primitive_Serialises_To_Bson_Decimal128_Correctly()
    {
        // Assign
        var example = DecimalWith2Digits.Example;
        var expected = (DecimalWith2Digits)example;
        var expectedAsDecimal128 = new Decimal128(expected);
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new BsonIDecimalSerializer<DecimalWith2Digits>(BsonType.Decimal128);
        bsonWriter.Setup(r => r.WriteDecimal128(It.IsAny<Decimal128>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteDecimal128(expectedAsDecimal128), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Default_Bson_String_Correctly()
    {
        // Assign
        var example = DecimalWith2Digits.Example;
        var expected = (DecimalWith2Digits)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalWith2Digits>());
        bsonWriter.Setup(r => r.WriteString(It.IsAny<string>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(expected), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Decimal128_Correctly()
    {
        // Assign
        var example = DecimalWith2Digits.Example;
        var expected = (DecimalWith2Digits)example;
        var expectedAsDecimal128 = new Decimal128(expected);
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalWith2Digits>(BsonType.Decimal128));
        bsonWriter.Setup(r => r.WriteDecimal128(It.IsAny<Decimal128>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteDecimal128(expectedAsDecimal128), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly_When_Null()
    {
        // Assign
        var expected = (DecimalWith2Digits?)null;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new BsonIDecimalSerializer<DecimalWith2Digits>());
        bsonWriter.Setup(r => r.WriteNull());

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(null), Times.Never);
        bsonWriter.Verify(r => r.WriteNull(), Times.Once);
    }
}
