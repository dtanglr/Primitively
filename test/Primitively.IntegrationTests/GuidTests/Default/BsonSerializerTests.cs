using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.Default;

public class BsonSerializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>();
        bsonWriter.Setup(r => r.WriteString(It.IsAny<string>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(example), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>());
        bsonWriter.Setup(r => r.WriteString(It.IsAny<string>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(example), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly_When_Null()
    {
        // Assign
        var expected = (DefaultThirtySixDigitsWithHyphens?)null;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>());
        bsonWriter.Setup(r => r.WriteNull());

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(null), Times.Never);
        bsonWriter.Verify(r => r.WriteNull(), Times.Once);
    }
}
