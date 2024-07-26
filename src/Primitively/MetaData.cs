using System;

namespace Primitively;

/// <summary>
/// Contains metadata about various Primitively types.
/// </summary>
internal static class MetaData
{
    /// <summary>
    /// Contains metadata about the DateOnly Primitively type.
    /// </summary>
    public static class DateOnly
    {
        public static readonly string Interface = typeof(IDateOnly).FullName;
        public static readonly string Type = typeof(DateTime).FullName;
        public static readonly string InfoType = typeof(DateOnlyInfo).FullName;

        /// <summary>
        /// Contains metadata about the ISO 8601 format for the DateOnly Primitively type.
        /// </summary>
        public static class Iso8601
        {
            public const string Example = "2022-12-31";
            public const string Format = "yyyy-MM-dd";
            public const int Length = 10;
        }
    }

    /// <summary>
    /// Contains metadata about the Guid Primitively type.
    /// </summary>
    public static class Guid
    {
        public static readonly string Interface = typeof(IGuid).FullName;
        public static readonly string Type = typeof(System.Guid).FullName;
        public static readonly string InfoType = typeof(GuidInfo).FullName;

        /// <summary>
        /// Contains metadata about the 'B' format for the Guid Primitively type.
        /// </summary>
        public static class B
        {
            public const string Example = "{2c48c152-7cb7-4f51-8f01-704454f36e60}";
            public const string Format = "B";
            public const int Length = 38;
        }

        /// <summary>
        /// Contains metadata about the 'D' format for the Guid Primitively type.
        /// </summary>
        public static class D
        {
            public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
            public const string Format = "D";
            public const int Length = 36;
        }

        /// <summary>
        /// Contains metadata about the 'N' format for the Guid Primitively type.
        /// </summary>
        public static class N
        {
            public const string Example = "2c48c1527cb74f518f01704454f36e60";
            public const string Format = "N";
            public const int Length = 32;
        }

        /// <summary>
        /// Contains metadata about the 'P' format for the Guid Primitively type.
        /// </summary>
        public static class P
        {
            public const string Example = "(2c48c152-7cb7-4f51-8f01-704454f36e60)";
            public const string Format = "P";
            public const int Length = 38;
        }

        /// <summary>
        /// Contains metadata about the 'X' format for the Guid Primitively type.
        /// </summary>
        public static class X
        {
            public const string Example = "{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}";
            public const string Format = "X";
            public const int Length = 68;
        }
    }

    /// <summary>
    /// Contains metadata about various Numeric Primitively types.
    /// </summary>
    public static class Numeric
    {
        public static class FloatingPoint
        {
            public const int Digits = -1;
            public const int MinDigits = 0;
            public const MidpointRounding Mode = MidpointRounding.ToEven;

            /// <summary>
            /// Contains metadata about the Decimal Primitively type.
            /// </summary>
            public static class Decimal
            {
                public static readonly string Example = (decimal.MaxValue / 2).ToString();
                public static readonly string Interface = typeof(IDecimal).FullName;
                public static readonly string JsonReaderMethod = "TryGetDecimal";
                public static readonly decimal Maximum = decimal.MaxValue;
                public static readonly decimal Minimum = decimal.MinValue;
                public static readonly string Type = typeof(decimal).FullName;
                public static readonly int MaxDigits = 28;
                public static readonly string InfoType = typeof(DecimalInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the Double Primitively type.
            /// </summary>
            public static class Double
            {
                public static readonly string Example = (double.MaxValue / 2).ToString("E");
                public static readonly string Interface = typeof(IDouble).FullName;
                public static readonly string JsonReaderMethod = "TryGetDouble";
                public static readonly double Maximum = double.MaxValue;
                public static readonly double Minimum = double.MinValue;
                public static readonly string Type = typeof(double).FullName;
                public static readonly int MaxDigits = 15;
                public static readonly string InfoType = typeof(DoubleInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the Single (float) Primitively type.
            /// </summary>
            public static class Single
            {
                public static readonly string Example = (float.MaxValue / 2).ToString("E");
                public static readonly string Interface = typeof(ISingle).FullName;
                public static readonly string JsonReaderMethod = "TryGetSingle";
                public static readonly float Maximum = float.MaxValue;
                public static readonly float Minimum = float.MinValue;
                public static readonly string Type = typeof(float).FullName;
                public static readonly int MaxDigits = 6;
                public static readonly string InfoType = typeof(SingleInfo).FullName;
            }
        }

        public static class Integer
        {
            /// <summary>
            /// Contains metadata about the Byte Primitively type.
            /// </summary>
            public static class Byte
            {
                public static readonly string Example = $"{byte.MaxValue / 2}";
                public static readonly string Interface = typeof(IByte).FullName;
                public static readonly string JsonReaderMethod = "TryGetByte";
                public static readonly decimal Maximum = byte.MaxValue;
                public static readonly decimal Minimum = byte.MinValue;
                public static readonly string Type = typeof(byte).FullName;
                public static readonly string InfoType = typeof(ByteInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the Int Primitively type.
            /// </summary>
            public static class Int
            {
                public static readonly string Example = $"{int.MaxValue / 2}";
                public static readonly string Interface = typeof(IInt).FullName;
                public static readonly string JsonReaderMethod = "TryGetInt32";
                public static readonly decimal Maximum = int.MaxValue;
                public static readonly decimal Minimum = int.MinValue;
                public static readonly string Type = typeof(int).FullName;
                public static readonly string InfoType = typeof(IntInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the Long Primitively type.
            /// </summary>
            public static class Long
            {
                public static readonly string Example = $"{long.MaxValue / 2}";
                public static readonly string Interface = typeof(ILong).FullName;
                public static readonly string JsonReaderMethod = "TryGetInt64";
                public static readonly decimal Maximum = long.MaxValue;
                public static readonly decimal Minimum = long.MinValue;
                public static readonly string Type = typeof(long).FullName;
                public static readonly string InfoType = typeof(LongInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the SByte Primitively type.
            /// </summary>
            public static class SByte
            {
                public static readonly string Example = $"{sbyte.MaxValue / 2}";
                public static readonly string Interface = typeof(ISByte).FullName;
                public static readonly string JsonReaderMethod = "TryGetSByte";
                public static readonly decimal Maximum = sbyte.MaxValue;
                public static readonly decimal Minimum = sbyte.MinValue;
                public static readonly string Type = typeof(sbyte).FullName;
                public static readonly string InfoType = typeof(SByteInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the Short Primitively type.
            /// </summary>
            public static class Short
            {
                public static readonly string Example = $"{short.MaxValue / 2}";
                public static readonly string Interface = typeof(IShort).FullName;
                public static readonly string JsonReaderMethod = "TryGetInt16";
                public static readonly decimal Maximum = short.MaxValue;
                public static readonly decimal Minimum = short.MinValue;
                public static readonly string Type = typeof(short).FullName;
                public static readonly string InfoType = typeof(ShortInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the UInt Primitively type.
            /// </summary>
            public static class UInt
            {
                public static readonly string Example = $"{uint.MaxValue / 2}";
                public static readonly string Interface = typeof(IUInt).FullName;
                public static readonly string JsonReaderMethod = "TryGetUInt32";
                public static readonly decimal Maximum = uint.MaxValue;
                public static readonly decimal Minimum = uint.MinValue;
                public static readonly string Type = typeof(uint).FullName;
                public static readonly string InfoType = typeof(UIntInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the ULong Primitively type.
            /// </summary>
            public static class ULong
            {
                public static readonly string Example = $"{ulong.MaxValue / 2}";
                public static readonly string Interface = typeof(IULong).FullName;
                public static readonly string JsonReaderMethod = "TryGetUInt64";
                public static readonly decimal Maximum = ulong.MaxValue;
                public static readonly decimal Minimum = ulong.MinValue;
                public static readonly string Type = typeof(ulong).FullName;
                public static readonly string InfoType = typeof(ULongInfo).FullName;
            }

            /// <summary>
            /// Contains metadata about the UShort Primitively type.
            /// </summary>
            public static class UShort
            {
                public static readonly string Example = $"{ushort.MaxValue / 2}";
                public static readonly string Interface = typeof(IUShort).FullName;
                public static readonly string JsonReaderMethod = "TryGetUInt16";
                public static readonly decimal Maximum = ushort.MaxValue;
                public static readonly decimal Minimum = ushort.MinValue;
                public static readonly string Type = typeof(ushort).FullName;
                public static readonly string InfoType = typeof(UShortInfo).FullName;
            }
        }
    }

    /// <summary>
    /// Contains metadata about the String Primitively type.
    /// </summary>
    public static class String
    {
        public static readonly string Interface = typeof(IString).FullName;
        public static readonly string Type = typeof(string).FullName;
        public static readonly string InfoType = typeof(StringInfo).FullName;
    }
}
