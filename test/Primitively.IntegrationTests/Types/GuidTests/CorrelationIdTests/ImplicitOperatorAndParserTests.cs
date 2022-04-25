using System;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.GuidTests.CorrelationIdTests;

public class ImplicitOperatorAndParserTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("11f72a78-ce37-4ad1-9f87-535b2c15e94d", true)]
    [InlineData("9BC12195-B4A9-4880-B526-A0BE96EDDA08", true)]
    public void ConvertFromThisToThatWithExpectedResults(string from, bool hasValue = default)
    {
        var expectedGuid = hasValue ? Guid.Parse(from) : default;
        var expectedString = expectedGuid.ToString("D");

        var @this = (CorrelationId)from;
        string to = @this;
        var that = CorrelationId.Parse(to);
        var and = new CorrelationId(that.Value);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        @this.Value.Should().Be(expectedGuid);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        that.Value.Should().Be(expectedGuid);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        and.Value.Should().Be(expectedGuid);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
    }
}
