﻿using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.StringTests;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = SevenDigits.Example;
        var expected = (SevenDigits)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new BsonIStringSerializer<SevenDigits>();
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.String);
        bsonReader.Setup(r => r.ReadString()).Returns(example);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadString(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = SevenDigits.Example;
        var expected = (SevenDigits)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonIStringSerializer<SevenDigits>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.String);
        bsonReader.Setup(r => r.ReadString()).Returns(example);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadString(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new BsonIStringSerializer<SevenDigits>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().BeNull();
        bsonReader.Verify(r => r.ReadString(), Times.Never);
    }
}
