using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using static Primitively.FluentValidation.Extensions;

namespace Primitively.IntegrationTests.Types.DateOnlyTests;

public class FluentValidationTests
{
    private readonly Validator _validator;

    private record Sut(BirthDate Property, BirthDate? NullableProperty);

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
    [InlineData("0001-01-01")]
    [InlineData("9999-99-99")]
    [InlineData("2022-02-31")]
    [InlineData("2022-31-01")]
    [InlineData("2022/01/01")]
    [InlineData("01/01/2022")]
    [InlineData("31/01/2022")]
    [InlineData("01/31/2022")]
    [InlineData("2022-01-01", true, true)]
    public void ConvertFromThisToThatWithExpectedResults(string value, bool nonNullableIsValid = false, bool nullableIsValid = false)
    {
        var sut = new Sut(BirthDate.Parse(value), value is null ? null : BirthDate.Parse(value));
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
