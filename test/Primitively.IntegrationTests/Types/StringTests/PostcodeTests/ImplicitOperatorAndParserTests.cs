using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.PostcodeTests;

public class ImplicitOperatorAndParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("L")]
    [InlineData("LS")]
    [InlineData("LS1")]
    [InlineData("LS1 ")]
    [InlineData("LS1 6")]
    [InlineData("LS1 6A")]
    [InlineData("LS1 6AEE")]
    [InlineData("LS1 6AEEF")]
    [InlineData("LS16AEE")]
    [InlineData("LS16AEE ")]
    [InlineData("LS 16 AEE ")]
    [InlineData("EC1A 1BB", true)]
    [InlineData("EC1A1BB", true)]
    [InlineData("W1A 0AX", true)]
    [InlineData("W1A0AX", true)]
    [InlineData("M1 1AE", true)]
    [InlineData("M11AE", true)]
    [InlineData("B33 8TH", true)]
    [InlineData("B338TH", true)]
    [InlineData("CR2 6XH", true)]
    [InlineData("CR26XH", true)]
    [InlineData("DN55 1PT", true)]
    [InlineData("DN551PT", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expected = hasValue ? from : default;

        var @this = (Postcode)from;
        string to = @this;
        var that = Postcode.Parse(to);

        @this.HasValue.Should().Be(hasValue);
        @this.Value.Should().Be(expected);
        @this.ToString().Should().Be(expected);
        to.Should().Be(expected);
        that.HasValue.Should().Be(hasValue);
        that.Value.Should().Be(expected);
        that.ToString().Should().Be(expected);
    }
}
