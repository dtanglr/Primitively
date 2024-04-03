using Primitively;

// https://en.wikipefia.org/wiki/Celsius
// https://en.wikipefia.org/wiki/Conversion_of_scales_of_temperature

namespace Acme.TestLib2.Single;

[Single(2, Minimum = 0f)]
public partial record struct Rankine : ITemperature<Rankine>
{
    public static Rankine AbsoluteZero => new(0f);

    public static Rankine WaterMeltingPoint => new(491.67f);

    public static Rankine WaterBoilingPoint => new(671.64102f);

    public static explicit operator Celsius(Rankine value) => new((5f / 9f * value) - 273.15f);
    public static explicit operator Fahrenheit(Rankine value) => new(value - 459.67f);
    public static explicit operator Kelvin(Rankine value) => new(5f / 9f * value);
}
