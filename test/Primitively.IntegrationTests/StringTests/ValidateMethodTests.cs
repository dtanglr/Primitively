using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.StringTests;

public class ValidateMethodTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("G12345679")]
    [InlineData("123456789")]
    [InlineData("1234567890", true)]
    [InlineData("0123456789", true)]
    [InlineData("ABCDEFGHIJ", true)]
    public void ConvertFromThisToThatWithExpectedResults(string? value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = ValidatableString.Parse(value);

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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@example")]
    [InlineData("test@example.")]
    [InlineData("test@example.com", true)]
    public void ConvertFromThisToThatWithExpectedResults2(string? value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = Email.Parse(value);

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
