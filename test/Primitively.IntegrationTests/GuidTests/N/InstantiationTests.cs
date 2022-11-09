using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.GuidTests.N;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("00000000000000000000000000000000")]
    [InlineData("11f72a78ce374ad19f87535b2c15e94d", true)]
    [InlineData("9BC12195B4A94880B526A0BE96EDDA08", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expectedGuid = hasValue ? Guid.Parse(from) : default;
        var expectedString = expectedGuid.ToString("N");

        var @this = (ThirtyTwoDigits)from;
        string to = @this;
        var that = ThirtyTwoDigits.Parse(to);
        var and = new ThirtyTwoDigits(that);
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
