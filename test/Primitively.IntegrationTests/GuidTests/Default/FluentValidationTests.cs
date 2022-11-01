using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using static Primitively.FluentValidation.PrimitiveFluentValidationExtensions;

namespace Primitively.IntegrationTests.GuidTests.Default;

public class FluentValidationTests
{
    private readonly Validator _validator;

    private record Sut(DefaultThirtySixDigitsWithHyphens Property, DefaultThirtySixDigitsWithHyphens? NullableProperty);

    private class Validator : AbstractValidator<Sut> { }

    public FluentValidationTests()
    {
        _validator = new Validator();
        _validator.RuleFor(model => model.Property).MustBeValid();
        _validator.RuleFor(model => model.NullableProperty).MustBeValidOrEmpty();
    }

    [Theory]
    [InlineData(null, false, true)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("11f72a78-ce37-4ad1-9f87-535b2c15e94d", true, true)]
    [InlineData("9BC12195-B4A9-4880-B526-A0BE96EDDA08", true, true)]
    public void ConvertFromThisToThatWithExpectedResults(string value, bool nonNullableIsValid = false, bool nullableIsValid = false)
    {
        var sut = new Sut(DefaultThirtySixDigitsWithHyphens.Parse(value), value is null ? null : DefaultThirtySixDigitsWithHyphens.Parse(value));
        var result = _validator.TestValidate(sut);

        if (nonNullableIsValid)
        {
            result.ShouldNotHaveValidationErrorFor(x => x.Property);
        }
        else
        {
            result.ShouldHaveValidationErrorFor(x => x.Property);
        }

        if (nullableIsValid)
        {
            result.ShouldNotHaveValidationErrorFor(x => x.NullableProperty);
        }
        else
        {
            result.ShouldHaveValidationErrorFor(x => x.NullableProperty);
        }
    }
}
