using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.DateOnlyTests;

public class ValidateMethodTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("0001-01-01")]
    [InlineData("9999-99-99")]
    [InlineData("2022-02-31")]
    [InlineData("2022-31-01")]
    [InlineData("2022/01/01")]
    [InlineData("01/01/2022")]
    [InlineData("31/01/2022")]
    [InlineData("01/31/2022")]
    [InlineData(ValidatableDate.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = ValidatableDate.Parse(value);

        // Act
        var result = sut.Validate(validationContext);

        // Assert
        if (isValid)
        {
            result.Should().BeEmpty();
        }
        else
        {
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }
    }
}
