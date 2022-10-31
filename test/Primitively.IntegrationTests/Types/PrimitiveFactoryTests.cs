using System;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types;

public class PrimitiveFactoryTests
{
    [Theory]
    [InlineData(typeof(BirthDate), BirthDate.Example)]
    [InlineData(typeof(DeathDate), DeathDate.Example)]
    [InlineData(typeof(CorrelationId), CorrelationId.Example)]
    [InlineData(typeof(ListId), ListId.Example)]
    [InlineData(typeof(QueryId), QueryId.Example)]
    [InlineData(typeof(RequestId), RequestId.Example)]
    [InlineData(typeof(SiteCollectionId), SiteCollectionId.Example)]
    [InlineData(typeof(EightDigits), EightDigits.Example)]
    [InlineData(typeof(ElevenDigits), ElevenDigits.Example)]
    [InlineData(typeof(NineDigits), NineDigits.Example)]
    [InlineData(typeof(SevenDigits), SevenDigits.Example)]
    [InlineData(typeof(TenDigits), TenDigits.Example)]
    public void CreateMethod_ReturnsCorrectType_WhenMatchFound(Type modelType, string value)
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var result = factory.Create(modelType, value);

        // Assert
        result.Should().BeOfType(modelType);
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void CreateMethod_ReturnsError_WhenNoMatchFound()
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var result = () => factory.Create(typeof(Guid), Guid.NewGuid().ToString());

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(typeof(BirthDate), BirthDate.Example)]
    [InlineData(typeof(DeathDate), DeathDate.Example)]
    [InlineData(typeof(CorrelationId), CorrelationId.Example)]
    [InlineData(typeof(ListId), ListId.Example)]
    [InlineData(typeof(QueryId), QueryId.Example)]
    [InlineData(typeof(RequestId), RequestId.Example)]
    [InlineData(typeof(SiteCollectionId), SiteCollectionId.Example)]
    [InlineData(typeof(EightDigits), EightDigits.Example)]
    [InlineData(typeof(ElevenDigits), ElevenDigits.Example)]
    [InlineData(typeof(NineDigits), NineDigits.Example)]
    [InlineData(typeof(SevenDigits), SevenDigits.Example)]
    [InlineData(typeof(TenDigits), TenDigits.Example)]
    public void TryCreateMethod_ReturnsCorrectType_WhenMatchFound(Type modelType, string value)
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var created = factory.TryCreate(modelType, value, out var result);

        // Assert
        result.Should().BeOfType(modelType);
        result.HasValue.Should().BeTrue();
        created.Should().BeTrue();
    }

    [Fact]
    public void TryCreateMethod_ReturnsFalse_WhenNoMatchFound()
    {
        // Arrange
        var factory = new PrimitiveFactory();

        // Act
        var created = factory.TryCreate(typeof(Guid), Guid.NewGuid().ToString(), out var result);

        // Assert
        result.Should().BeNull();
        created.Should().BeFalse();
    }
}
