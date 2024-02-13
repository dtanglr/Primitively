
    public class TypeConverter : global::System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(global::PRIMITIVE_VALUE_TYPE?) ||
            sourceType == typeof(global::PRIMITIVE_VALUE_TYPE) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                global::PRIMITIVE_VALUE_TYPE integer => new PRIMITIVE_TYPE(integer),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(global::PRIMITIVE_VALUE_TYPE) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value, global::System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(global::PRIMITIVE_VALUE_TYPE))
                {
                    return (global::PRIMITIVE_VALUE_TYPE)primitive;
                }

                if (destinationType == typeof(string))
                {
                    return primitive.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
