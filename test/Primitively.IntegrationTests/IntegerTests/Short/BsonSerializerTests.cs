﻿using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Moq;
using Primitively.MongoDb;
using Xunit;

namespace Primitively.IntegrationTests.IntegerTests.Short;

public class BsonSerializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = ShortId.Example;
        var expected = (ShortId)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new PrimitiveSerializer<ShortId>();
        bsonWriter.Setup(r => r.WriteInt32(It.IsAny<int>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteInt32(expected), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly()
    {
        // Assign
        var example = ShortId.Example;
        var expected = (ShortId)example;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new NullablePrimitiveSerializer<ShortId>();
        bsonWriter.Setup(r => r.WriteInt32(It.IsAny<int>()));

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteInt32(expected), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Serialises_To_Bson_Correctly_When_Null()
    {
        // Assign
        var expected = (ShortId?)null;
        var bsonWriter = new Mock<IBsonWriter>();
        var context = BsonSerializationContext.CreateRoot(bsonWriter.Object);
        var serializer = new NullablePrimitiveSerializer<ShortId>();
        bsonWriter.Setup(r => r.WriteNull());

        // Act
        serializer.Serialize(context, new BsonSerializationArgs(), expected);

        // Assert
        bsonWriter.Verify(r => r.WriteInt32(It.IsAny<int>()), Times.Never);
        bsonWriter.Verify(r => r.WriteNull(), Times.Once);
    }
}
