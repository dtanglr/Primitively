using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types;

public class PrimitiveRepositoryTests
{
    [Theory]
    // DateOnly
    [InlineData(typeof(BirthDate), typeof(DateOnlyInfo))]
    [InlineData(typeof(DeathDate), typeof(DateOnlyInfo))]
    // Guid
    [InlineData(typeof(CorrelationId), typeof(GuidInfo))]
    [InlineData(typeof(ListId), typeof(GuidInfo))]
    [InlineData(typeof(QueryId), typeof(GuidInfo))]
    [InlineData(typeof(RequestId), typeof(GuidInfo))]
    [InlineData(typeof(SiteCollectionId), typeof(GuidInfo))]
    // String
    [InlineData(typeof(EightDigits), typeof(StringInfo))]
    [InlineData(typeof(ElevenDigits), typeof(StringInfo))]
    [InlineData(typeof(NineDigits), typeof(StringInfo))]
    [InlineData(typeof(SevenDigits), typeof(StringInfo))]
    [InlineData(typeof(TenDigits), typeof(StringInfo))]
    public void GetTypeMethod_ReturnsCorrectType_WhenMatchFound(Type type, Type resultType)
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var result = repo.GetType(type);

        // Assert
        result.Should().BeOfType(resultType);
    }

    [Fact]
    public void GetTypeMethod_ReturnsError_WhenNoMatchFound()
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var result = () => repo.GetType(typeof(Guid));

        // Assert
        result.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    // DateOnly
    [InlineData(typeof(BirthDate), typeof(DateOnlyInfo))]
    [InlineData(typeof(DeathDate), typeof(DateOnlyInfo))]
    // Guid
    [InlineData(typeof(CorrelationId), typeof(GuidInfo))]
    [InlineData(typeof(ListId), typeof(GuidInfo))]
    [InlineData(typeof(QueryId), typeof(GuidInfo))]
    [InlineData(typeof(RequestId), typeof(GuidInfo))]
    [InlineData(typeof(SiteCollectionId), typeof(GuidInfo))]
    // String
    [InlineData(typeof(EightDigits), typeof(StringInfo))]
    [InlineData(typeof(ElevenDigits), typeof(StringInfo))]
    [InlineData(typeof(NineDigits), typeof(StringInfo))]
    [InlineData(typeof(SevenDigits), typeof(StringInfo))]
    [InlineData(typeof(TenDigits), typeof(StringInfo))]
    public void TryGetTypeMethod_ReturnsCorrectType_WhenMatchFound(Type type, Type? resultType)
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var outcome = repo.TryGetType(type, out var result);

        // Assert
        outcome.Should().BeTrue();
        result.Should().BeOfType(resultType);
    }

    [Fact]
    public void TryGetTypeMethod_ReturnsFalse_WhenNoMatchFound()
    {
        // Arrange
        var repo = new PrimitiveRepository();

        // Act
        var outcome = repo.TryGetType(typeof(Guid), out var result);

        // Assert
        outcome.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GetTypesMethod_ReturnsCollectionOfPrimitiveInfoType()
    {
        // Arrange
        var repo = new PrimitiveRepository();
        var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IPrimitive)))
            .ToList();

        // Act
        var result = repo.GetTypes();

        // Assert
        result.Should().HaveCount(types.Count);
    }

    [Theory]
    [InlineData(typeof(IDateOnly))]
    [InlineData(typeof(IGuid))]
    [InlineData(typeof(IString))]
    public void GetTypesOfTMethod_ReturnsCollectionOfPrimitiveInfoType(Type type)
    {
        // Arrange
        var repo = new PrimitiveRepository();
        var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IPrimitive)) && t.IsAssignableTo(type))
            .ToList();

        // Act
        IReadOnlyCollection<PrimitiveInfo> result = type.Name switch
        {
            nameof(IDateOnly) => repo.GetTypes<DateOnlyInfo>(),
            nameof(IGuid) => repo.GetTypes<GuidInfo>(),
            nameof(IString) => repo.GetTypes<StringInfo>(),
            _ => throw new NotImplementedException()
        };

        // Assert
        result.Should().HaveCount(types.Count);
    }
}
