
#if NET6_0_OR_GREATER
    public class TypeConverter : global::System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(global::System.DateOnly?) ||
            sourceType == typeof(global::System.DateOnly) ||
            sourceType == typeof(global::System.DateTime?) ||
            sourceType == typeof(global::System.DateTime) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                global::System.DateOnly dateOnly => new PRIMITIVE_TYPE(dateOnly),
                global::System.DateTime dateTime => new PRIMITIVE_TYPE(dateTime),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(global::System.DateOnly) || sourceType == typeof(global::System.DateTime) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value, global::System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(global::System.DateOnly))
                {
                    return (global::System.DateOnly)primitive;
                }

                if (destinationType == typeof(global::System.DateTime))
                {
                    return (global::System.DateTime)primitive;
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
    public class TypeConverter : global::System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType) =>
            sourceType == typeof(string) ||
            sourceType == typeof(global::System.DateTime?) ||
            sourceType == typeof(global::System.DateTime) ||
            base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value)
        {
            return value switch
            {
                string @string => new PRIMITIVE_TYPE(@string),
                global::System.DateTime dateTime => new PRIMITIVE_TYPE(dateTime),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(global::System.DateTime) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertTo(global::System.ComponentModel.ITypeDescriptorContext context, global::System.Globalization.CultureInfo culture, object value, global::System.Type destinationType)
        {
            if (value is PRIMITIVE_TYPE primitive)
            {
                if (destinationType == typeof(global::System.DateTime))
                {
                    return (global::System.DateTime)primitive;
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
