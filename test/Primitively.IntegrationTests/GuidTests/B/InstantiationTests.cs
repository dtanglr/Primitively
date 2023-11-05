using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.B;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("{00000000-0000-0000-0000-000000000000}")]
    [InlineData("{11f72a78-ce37-4ad1-9f87-535b2c15e94d}", true)]
    [InlineData("{9BC12195-B4A9-4880-B526-A0BE96EDDA08}", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expectedGuid = hasValue ? Guid.Parse(from) : Guid.Empty;
        var expectedString = expectedGuid.ToString("B");

        var @this = (ThirtyEightDigitsWithHyphensAndBraces)from;
        string to = @this;
        var that = ThirtyEightDigitsWithHyphensAndBraces.Parse(to);
        var and = new ThirtyEightDigitsWithHyphensAndBraces(that);
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
