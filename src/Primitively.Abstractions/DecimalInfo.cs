﻿namespace Primitively;

/// <summary>
/// This class represents metadata properties common to all source generated Primitively numeric types.
/// </summary>
/// <param name="Type">The .NET type of the Primitively type.</param>
/// <param name="Example">An optional example of the integer.</param>
/// <param name="CreateFrom">A function that creates an instance of the Primitively type from a string.</param>
/// <param name="Minimum">The minimum value that can be set on the source generated Primitively type.</param>
/// <param name="Maximum">The maximum value that can be set on the source generated Primitively type.</param>
/// <param name="Digits">The number of fractional digits in the value on the source generated Primitively type</param>
/// <param name="Mode">The rounding specification for how to round value of the source generated Primitively type 
/// if it is midway between two other numbers.</param>
public sealed record DecimalInfo(
    Type Type,
    string? Example,
    Func<string?, IPrimitive> CreateFrom,
    decimal Minimum,
    decimal Maximum,
    int Digits,
    MidpointRounding Mode)
    : NumericInfo<decimal>(DataType.Decimal, Type, Example, CreateFrom, Minimum, Maximum);
