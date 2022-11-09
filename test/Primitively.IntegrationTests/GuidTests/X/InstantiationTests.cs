using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.X;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}")]
    [InlineData("{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}", true)]
    [InlineData("{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x50}}", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expectedGuid = hasValue ? Guid.Parse(from) : default;
        var expectedString = expectedGuid.ToString("X");

        var @this = (SixtyEightHexadecimalsWithHyphensAndBraces)from;
        string to = @this;
        var that = SixtyEightHexadecimalsWithHyphensAndBraces.Parse(to);
        var and = new SixtyEightHexadecimalsWithHyphensAndBraces(that);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        ((Guid)@this).Should().Be(expectedGuid);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        ((Guid)that).Should().Be(expectedGuid);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        ((Guid)and).Should().Be(expectedGuid);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
    }
}
