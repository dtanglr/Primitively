using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.N;

public class ValidateMethodTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("00000000000000000000000000000000")]
    [InlineData("11f72a78ce374ad19f87535b2c15e94d", true)]
    [InlineData("9BC12195B4A94880B526A0BE96EDDA08", true)]
    public void ConvertFromThisToThatWithExpectedResults(string? value, bool isValid = false)
    {
        // Arrange
        var validationContext = new ValidationContext(this);
        var sut = ThirtyTwoDigits.Parse(value);

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
