using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.P;

public class ValidateMethodTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("(00000000-0000-0000-0000-000000000000)")]
    [InlineData("(11f72a78-ce37-4ad1-9f87-535b2c15e94d)", true)]
    [InlineData("(9BC12195-B4A9-4880-B526-A0BE96EDDA08)", true)]
    public void ConvertFromThisToThatWithExpectedResults(string value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = ThirtyEightDigitsWithHyphensAndParentheses.Parse(value);

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
