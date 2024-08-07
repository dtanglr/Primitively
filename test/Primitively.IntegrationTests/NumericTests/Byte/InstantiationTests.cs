﻿using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.NumericTests.Byte;

public class InstantiationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("-1")]
    [InlineData("0", true)]
    [InlineData("00", true)]
    [InlineData("001", true)]
    [InlineData(ByteId.Example, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? from, bool hasValue = false)
    {
        var @this = ByteId.Parse(from);

        @this.HasValue.Should().Be(hasValue);
    }
}
