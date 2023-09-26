using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDb.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.DateOnlyTests;

public class BsonSerializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = (BirthDate)BirthDate.Example;
        DateOnly dateOnly = example;
        var ticks = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day).Ticks; // TODO: Add Year, Month, Day to IDateOnly
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new DateOnlyBsonSerializer<BirthDate>();
        bsonWriter.Setup(r => r.WriteDateTime(It.IsAny<long>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), example);

        // Assert
        bsonWriter.Verify(r => r.WriteDateTime(ticks), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = (BirthDate)BirthDate.Example;
        DateOnly dateOnly = example;
        var ticks = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day).Ticks; // TODO: Add Year, Month, Day to IDateOnly
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new DateOnlyBsonSerializer<BirthDate>());
        bsonWriter.Setup(r => r.WriteString(It.IsAny<string>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), example);

        // Assert
        bsonWriter.Verify(r => r.WriteDateTime(ticks), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly_When_Null()
    {
        // Assign
        var expected = (BirthDate?)null;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = NullableSerializer.Create(new DateOnlyBsonSerializer<BirthDate>());
        bsonWriter.Setup(r => r.WriteNull());

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteNull(), Times.Once);
    }
}
