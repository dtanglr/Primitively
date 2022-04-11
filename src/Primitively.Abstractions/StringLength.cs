using System;
using System.ComponentModel;
using System.Globalization;

namespace Primitively;

[TypeConverter(typeof(StringLengthTypeConverter))]
public class StringLength : IStringLength
{
    public StringLength(int length) => Length = length;

    public int Length { get; }

    private class StringLengthTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => value switch
        {
            int @int => new StringLength(@int),
            _ => base.ConvertFrom(context, culture, value),
        };
    }

    public static implicit operator StringLength(int value) => new(value);
    public static implicit operator int(StringLength value) => value.Length;
}
