
public class PRIMITIVE_TYPETypeConverter : System.ComponentModel.TypeConverter
{
    public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) =>
        sourceType == typeof(string) ||
        sourceType == typeof(System.Guid?) ||
        sourceType == typeof(System.Guid) ||
        base.CanConvertFrom(context, sourceType);

    public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        return value switch
        {
            string @string => PRIMITIVE_TYPE.Parse(@string),
            System.Guid @guid => new PRIMITIVE_TYPE(@guid),
            _ => base.ConvertFrom(context, culture, value),
        };
    }

    public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(System.Guid) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
    {
        if (value is PRIMITIVE_TYPE primitive)
        {
            if (destinationType == typeof(System.Guid))
            {
                return (System.Guid)primitive;
            }

            if (destinationType == typeof(string))
            {
                return primitive.ToString();
            }
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
