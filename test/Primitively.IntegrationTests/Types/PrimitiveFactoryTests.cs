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
    public void FactoryCreateMethod_ReturnsCorrectType_WithAValidValue(Type modelType, string value)
    {
        // Act
        var result = PrimitiveFactory.Create(modelType, value);

        // Assert
        result.Should().BeOfType(modelType);
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void FactoryCreateMethod_ReturnsError_WhenNoMatchingType()
    {
        // Act
        var result = () => PrimitiveFactory.Create(typeof(Guid), Guid.NewGuid().ToString());

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }
}
