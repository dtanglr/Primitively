using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.NominatedPharmacyCodeTests;

public class ImplicitOperatorAndParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("Y")]
    [InlineData("Y1")]
    [InlineData("Y12", true)]
    [InlineData("Y123", true)]
    [InlineData("Y1234", true)]
    [InlineData("Y12345", true)]
    [InlineData("Y123456", true)]
    [InlineData("Y1234567", true)]
    [InlineData("Y12345678", true)]
    [InlineData("Y123456789", true)]
    [InlineData("Y1234567890")]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expected = hasValue ? from : default;

        var @this = (NominatedPharmacyCode)from;
        string to = @this;
        var that = NominatedPharmacyCode.Parse(to);

        @this.HasValue.Should().Be(hasValue);
        @this.Value.Should().Be(expected);
        @this.ToString().Should().Be(expected);
        to.Should().Be(expected);
        that.HasValue.Should().Be(hasValue);
        that.Value.Should().Be(expected);
        that.ToString().Should().Be(expected);
    }
}
