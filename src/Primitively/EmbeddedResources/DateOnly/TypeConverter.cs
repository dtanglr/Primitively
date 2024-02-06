
#if NET6_0_OR_GREATER
    public class TypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(System.DateOnly?) ||
            sourceType == typeof(System.DateOnly) ||
            sourceType == typeof(System.DateTime?) ||
            sourceType == typeof(System.DateTime) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                System.DateOnly dateOnly => new PRIMITIVE_TYPE(dateOnly),
                System.DateTime dateTime => new PRIMITIVE_TYPE(dateTime),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(System.DateOnly) || sourceType == typeof(System.DateTime) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(System.DateOnly))
                {
                    return (System.DateOnly)primitive;
                }

                if (destinationType == typeof(System.DateTime))
                {
                    return (System.DateTime)primitive;
                }

                if (destinationType == typeof(string))
                {
                    return primitive.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
#else
    public class TypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(System.DateTime?) ||
            sourceType == typeof(System.DateTime) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                System.DateTime dateTime => new PRIMITIVE_TYPE(dateTime),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(System.DateTime) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(System.DateTime))
                {
                    return (System.DateTime)primitive;
                }

                if (destinationType == typeof(string))
                {
                    return primitive.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
#endif
