
    public class TypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(PRIMITIVE_VALUE_TYPE?) ||
            sourceType == typeof(PRIMITIVE_VALUE_TYPE) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                PRIMITIVE_VALUE_TYPE integer => new PRIMITIVE_TYPE(integer),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(PRIMITIVE_VALUE_TYPE) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(PRIMITIVE_VALUE_TYPE))
                {
                    return (PRIMITIVE_VALUE_TYPE)primitive;
                }

                if (destinationType == typeof(string))
                {
                    return primitive.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
