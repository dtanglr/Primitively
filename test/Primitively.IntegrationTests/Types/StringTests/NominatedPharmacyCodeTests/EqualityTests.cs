using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests.Types.StringTests.NominatedPharmacyCodeTests;

public class EqualityTests
{
    private const string Value = "Y12345";
    private const string OtherValue = "X12345";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData(Value)]
    public void WhenTheSameType_ThisEqualsThat(string value)
    {
        var @this = NominatedPharmacyCode.Parse(value);
        var that = NominatedPharmacyCode.Parse(value);

        // This == That
        @this.Equals(that).Should().BeTrue();
        @this.Equals((NominatedPharmacyCode)that.Value).Should().BeTrue();
        @this.Value?.Equals(that).Should().BeTrue();
        @this.Value?.Equals(that.Value).Should().BeTrue();
        (@this == that).Should().BeTrue();
        (@this.Value == that.Value).Should().BeTrue();
        (@this != that).Should().BeFalse();
        (@this.Value != that.Value).Should().BeFalse();

        // That == This
        that.Equals(@this).Should().BeTrue();
        that.Equals((NominatedPharmacyCode)@this.Value).Should().BeTrue();
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
        var @this = NominatedPharmacyCode.Parse(Value);
        var that = NominatedPharmacyCode.Parse(OtherValue);

        // This == That
        @this.Equals(that).Should().BeFalse();
        (@this == that).Should().BeFalse();
        (@this.Value == that).Should().BeFalse();
        (@this != that).Should().BeTrue();
        (@this.Value != that).Should().BeTrue();

        // That == This
        that.Equals(@this).Should().BeFalse();
        (that == @this).Should().BeFalse();
        (that == (NominatedPharmacyCode)@this.Value).Should().BeFalse();
        (that != @this).Should().BeTrue();
        (that != (NominatedPharmacyCode)@this.Value).Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(Value)]
    public void WhenTheOtherTypeWithSameValueType_ThisNotEqualsThat(string value)
    {
        var @this = NominatedPharmacyCode.Parse(value);
        var that = GphcNumber.Parse(value);

        // This != That
        @this.Equals(that).Should().BeFalse();

        // That != This
        that.Equals(@this).Should().BeFalse();
    }
}
