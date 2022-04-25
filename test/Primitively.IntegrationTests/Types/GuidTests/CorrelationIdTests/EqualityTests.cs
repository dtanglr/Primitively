using System;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.GuidTests.CorrelationIdTests;

public class EqualityTests
{
    private const string Value = "2c48c152-7cb7-4f51-8f01-704454f36e60";
    private const string OtherValue = "3c48c152-7cb7-4f51-8f01-704454f36e66";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = CorrelationId.Parse(value);
        var that = CorrelationId.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((CorrelationId)that.Value).Should().BeTrue();
        @this.Value.Equals(that).Should().BeTrue();
        @this.Value.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((CorrelationId)@this.Value).Should().BeTrue();
        that.Value.Equals(@this).Should().BeTrue();
        that.Value.Equals(@this.Value).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that.Value == @this.Value).Should().BeTrue();
        (that != @this).Should().BeFalse();
        (that.Value != @this.Value).Should().BeFalse();
    }

    [Fact]
    public void WhenTheSameType_ThisDoesNotEqualThat()
    {
        var @this = CorrelationId.Parse(Value);
        var that = CorrelationId.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this.Value == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        (@this.Value != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that == (CorrelationId)@this.Value).Should().BeFalse();
        (that != @this).Should().BeTrue();
        (that != (CorrelationId)@this.Value).Should().BeTrue();
    }

    [Fact]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat()
    {
        var @this = CorrelationId.Parse(Value);
        var that = RequestId.Parse(Value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
