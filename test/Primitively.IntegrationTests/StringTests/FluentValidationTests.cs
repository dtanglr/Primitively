using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using static Primitively.FluentValidation.PrimitiveFluentValidationExtensions;

namespace Primitively.IntegrationTests.StringTests;

public class FluentValidationTests
{
    private readonly Validator _validator;

    private record Sut(SevenDigits Property, SevenDigits? NullableProperty);

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
    [InlineData("G7123456")]
    [InlineData("123456")]
    [InlineData("1234567", true, true)]
    [InlineData("0123456", true, true)]
    public void ConvertFromThisToThatWithExpectedResults(string value, bool nonNullableIsValid = false, bool nullableIsValid = false)
    {
        var sut = new Sut(SevenDigits.Parse(value), value is null ? null : SevenDigits.Parse(value));
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
