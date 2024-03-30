using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Int;

public class ValidateMethodTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("-1", true)]
    [InlineData("0", true)]
    [InlineData("00", true)]
    [InlineData("001", true)]
    [InlineData(ValidatableInteger.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = ValidatableInteger.Parse(value);

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
