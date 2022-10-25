﻿using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.DateOnlyTests.BirthDateTests;

public class EqualityTests
{
    private const string Value = "2022-01-01";
    private const string OtherValue = "2021-01-01";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = BirthDate.Parse(value);
        var that = BirthDate.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this != that).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that != @this).Should().BeFalse();
    }

    [Fact]
    public void WhenTheSameType_ThisDoesNotEqualThat()
    {
        var @this = BirthDate.Parse(Value);
        var that = BirthDate.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that != @this).Should().BeTrue();
    }

    [Fact]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat()
    {
        var @this = BirthDate.Parse(Value);
        var that = DeathDate.Parse(Value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
