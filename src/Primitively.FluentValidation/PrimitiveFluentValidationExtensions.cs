using FluentValidation;

namespace Primitively.FluentValidation;

public static partial class PrimitiveFluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> MustBeValid<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        where TProperty : struct, IPrimitive
    {
        return (IRuleBuilderOptions<T, TProperty>)builder.Custom((value, context) => Validate(value, context));
    }

    public static IRuleBuilderOptions<T, TProperty?> MustBeValidOrEmpty<T, TProperty>(this IRuleBuilder<T, TProperty?> builder)
        where TProperty : struct, IPrimitive
    {
        return (IRuleBuilderOptions<T, TProperty?>)builder.Custom((value, context) =>
        {
            if (!value.HasValue)
            {
                return;
            }

            Validate(value.Value, context);
        });
    }

    private static void Validate<T>(IPrimitive value, ValidationContext<T> context)
    {
        if (!value.HasValue)
        {
            context.AddFailure($"'{context.PropertyName}' must be valid.");
        }
    }
}
