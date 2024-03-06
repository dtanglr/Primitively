using FluentValidation;

namespace Primitively.FluentValidation;

/// <summary>
/// The <see cref="PrimitiveFluentValidationExtensions"/> static class provides extension methods to <see cref="FluentValidation"/> for Primitively types.
/// </summary>
public static class PrimitiveFluentValidationExtensions
{
    /// <summary>
    /// Adds a validation rule to ensure the Primitively type is valid.
    /// </summary>
    /// <typeparam name="T">The type of object being validated.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated.</typeparam>
    /// <param name="builder">The rule builder.</param>
    /// <returns>The same rule builder instance so that multiple calls can be chained.</returns>
    public static IRuleBuilderOptions<T, TProperty> MustBeValid<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        where TProperty : struct, IPrimitive
    {
        return (IRuleBuilderOptions<T, TProperty>)builder.Custom((value, context) => Validate(value, context));
    }

    /// <summary>
    /// Adds a validation rule to ensure the Primitively type is valid or empty.
    /// </summary>
    /// <typeparam name="T">The type of object being validated.</typeparam>
    /// <typeparam name="TProperty">The type of the property being validated.</typeparam>
    /// <param name="builder">The rule builder.</param>
    /// <returns>The same rule builder instance so that multiple calls can be chained.</returns>
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

    /// <summary>
    /// Validates the Primitively type.
    /// </summary>
    /// <typeparam name="T">The type of object being validated.</typeparam>
    /// <param name="value">The Primitively type to validate.</param>
    /// <param name="context">The validation context.</param>
    private static void Validate<T>(IPrimitive value, ValidationContext<T> context)
    {
        if (!value.HasValue)
        {
            context.AddFailure($"'{context.PropertyPath}' must be valid.");
        }
    }
}
