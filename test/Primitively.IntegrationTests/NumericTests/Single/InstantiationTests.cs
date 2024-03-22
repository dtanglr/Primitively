﻿using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Single;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("0")]
    [InlineData("00")]
    [InlineData("001", true)]
    [InlineData(SingleId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = default)
    {
        var expectedInteger = hasValue ? SingleId.Parse(from) : default;
        var expectedString = expectedInteger.ToString();

        var @this = (SingleId)from;
        string to = @this;
        var that = SingleId.Parse(to);
        var and = new SingleId(that);
        string back = and;

        @this.HasValue.Should().Be(hasValue);
        @this.Should().Be(expectedInteger);
        @this.ToString().Should().Be(expectedString);
        to.Should().Be(expectedString);
        that.HasValue.Should().Be(hasValue);
        that.Should().Be(expectedInteger);
        that.ToString().Should().Be(expectedString);
        and.HasValue.Should().Be(hasValue);
        and.Should().Be(expectedInteger);
        and.ToString().Should().Be(expectedString);
        back.Should().Be(expectedString);
    }
}
