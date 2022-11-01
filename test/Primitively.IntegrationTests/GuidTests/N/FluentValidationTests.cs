using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using static Primitively.FluentValidation.PrimitiveFluentValidationExtensions;

namespace Primitively.IntegrationTests.GuidTests.N;

public class FluentValidationTests
{
    private readonly Validator _validator;

    private record Sut(ThirtyTwoDigits Property, ThirtyTwoDigits? NullableProperty);

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
    [InlineData("00000000000000000000000000000000")]
    [InlineData("11f72a78ce374ad19f87535b2c15e94d", true, true)]
    [InlineData("9BC12195B4A94880B526A0BE96EDDA08", true, true)]
    public void ConvertFromThisToThatWithExpectedResults(string value, bool nonNullableIsValid = false, bool nullableIsValid = false)
    {
        var sut = new Sut(ThirtyTwoDigits.Parse(value), value is null ? null : ThirtyTwoDigits.Parse(value));
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
