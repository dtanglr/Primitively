using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.Default;

public class BsonSerializerTests
{
    public static IEnumerable<object[]> GuidRepresentations()
    {
        yield return new object[] { GuidRepresentation.CSharpLegacy };
        yield return new object[] { GuidRepresentation.JavaLegacy };
        yield return new object[] { GuidRepresentation.PythonLegacy };
        yield return new object[] { GuidRepresentation.Standard };
    }

    [Theory]
    [MemberData(nameof(GuidRepresentations))]
    public void Non_Nullable_Primitive_Serialises_To_Bson_Correctly(GuidRepresentation representation)
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>(representation);
        bsonWriter.Setup(r => r.WriteBinaryData(It.IsAny<BsonBinaryData>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteBinaryData(It.IsAny<BsonBinaryData>()), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GuidRepresentations))]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly(GuidRepresentation representation)
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>(representation));
        bsonWriter.Setup(r => r.WriteBinaryData(It.IsAny<BsonBinaryData>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteBinaryData(It.IsAny<BsonBinaryData>()), Times.Once);
    }

    [Theory]
    [MemberData(nameof(GuidRepresentations))]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly_When_Null(GuidRepresentation representation)
    {
        // Assign
        var expected = (DefaultThirtySixDigitsWithHyphens?)null;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>(representation));
        bsonWriter.Setup(r => r.WriteNull());

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteString(null), Times.Never);
        bsonWriter.Verify(r => r.WriteNull(), Times.Once);
    }
}
