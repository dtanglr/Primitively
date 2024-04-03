using Primitively;

// https://en.wikipefia.org/wiki/Celsius
// https://en.wikipefia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Single;

[Single(2, Minimum = -459.67f)]
public partial record struct Fahrenheit : ITemperature<Fahrenheit>
{
    public static Fahrenheit AbsoluteZero => new(-459.67f);

    public static Fahrenheit WaterMeltingPoint => new(32f);

    public static Fahrenheit WaterBoilingPoint => new(211.97102f);

    public static explicit operator Celsius(Fahrenheit value) => new((value - 32f) * (5f / 9f));
    public static explicit operator Kelvin(Fahrenheit value) => new((value + 459.67f) * (5f / 9f));
    public static explicit operator Rankine(Fahrenheit value) => new(value + 459.67f);
}
