﻿using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using static Primitively.FluentValidation.PrimitiveFluentValidationExtensions;

namespace Primitively.IntegrationTests.NumericTests.UShort;

public class FluentValidationTests
{
    private readonly Validator _validator;

    private record Sut(UShortId Property, UShortId? NullableProperty);

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
    [InlineData("-1")]
    [InlineData("0", true, true)]
    [InlineData("00", true, true)]
    [InlineData("001", true, true)]
    [InlineData(UShortId.Example, true, true)]
    public void ConvertFromThisToThatWithExpectedResults(string? value, bool nonNullableIsValid = false, bool nullableIsValid = false)
    {
        var sut = new Sut(UShortId.Parse(value), value is null ? null : UShortId.Parse(value));
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
