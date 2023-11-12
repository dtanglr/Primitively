﻿using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Moq;
using Primitively.MongoDB.Bson.Serialization.Serializers;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.Default;

public class BsonDeserializerTests
{
    [Fact]
    public void Non_Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>();
        var bytes = GuidConverter.ToBytes(expected, GuidRepresentation.CSharpLegacy);
        var subType = GuidConverter.GetSubType(GuidRepresentation.CSharpLegacy);
        var binaryData = new BsonBinaryData(bytes, subType);
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Binary);
        bsonReader.Setup(r => r.ReadBinaryData()).Returns(binaryData);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadBinaryData(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly()
    {
        // Assign
        var example = DefaultThirtySixDigitsWithHyphens.Example;
        var expected = (DefaultThirtySixDigitsWithHyphens)example;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>());
        var bytes = GuidConverter.ToBytes(expected, GuidRepresentation.CSharpLegacy);
        var subType = GuidConverter.GetSubType(GuidRepresentation.CSharpLegacy);
        var binaryData = new BsonBinaryData(bytes, subType);
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Binary);
        bsonReader.Setup(r => r.ReadBinaryData()).Returns(binaryData);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
        bsonReader.Verify(r => r.ReadBinaryData(), Times.Once);
    }

    [Fact]
    public void Nullable_Primitive_Deserialises_From_Bson_Correctly_When_Bson_Null()
    {
        // Assign
        var expected = (DefaultThirtySixDigitsWithHyphens?)null;
        var bsonReader = new Mock<IBsonReader>();
        var context = BsonDeserializationContext.CreateRoot(bsonReader.Object);
        var serializer = NullableSerializer.Create(new GuidBsonSerializer<DefaultThirtySixDigitsWithHyphens>());
        bsonReader.Setup(r => r.GetCurrentBsonType()).Returns(BsonType.Null);

        // Act
        var result = serializer.Deserialize(context, new BsonDeserializationArgs());

        // Assert
        result.Should().Be(expected);
    }
}
