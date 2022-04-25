using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.NhsNumberTests;

public class EqualityTests
{
    private const string Value = "0123456789";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = NhsNumber.Parse(value);
        var that = NhsNumber.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((NhsNumber)that.Value).Should().BeTrue();
        @this.Value?.Equals(that).Should().BeTrue();
        @this.Value?.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((NhsNumber)@this.Value).Should().BeTrue();
        that.Value?.Equals(@this).Should().BeTrue();
        that.Value?.Equals(@this.Value).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that.Value == @this.Value).Should().BeTrue();
        (that != @this).Should().BeFalse();
        (that.Value != @this.Value).Should().BeFalse();
    }

    [Fact]
    public void WhenTheSameType_ThisDoesNotEqualThat()
    {
        var @this = NhsNumber.Parse(Value);
        var that = (NhsNumber)Value;

        // This == That
        @this.Equals(that).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        (that == @this).Should().BeTrue();
        (that == @this.Value).Should().BeTrue();
        (that != @this).Should().BeFalse();
        (that != @this.Value).Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string value)
    {
        var @this = NhsNumber.Parse(value);
        var that = Postcode.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
