using System;

namespace Primitively;

/// <summary>
/// Contains metadata about various Primitively types.
/// </summary>
internal readonly struct MetaData
{
    /// <summary>
    /// Contains metadata about the DateOnly Primitively type.
    /// </summary>
    public readonly struct DateOnly
    {
        public static readonly string Interface = typeof(IDateOnly).FullName;
        public static readonly string Type = typeof(System.DateTime).FullName;

        /// <summary>
        /// Contains metadata about the ISO 8601 format for the DateOnly Primitively type.
        /// </summary>
        public readonly struct Iso8601
        {
            public const string Example = "2022-12-31";
            public const string Format = "yyyy-MM-dd";
            public const int Length = 10;
        }
    }

    /// <summary>
    /// Contains metadata about the Guid Primitively type.
    /// </summary>
    public readonly struct Guid
    {
        public static readonly string Interface = typeof(IGuid).FullName;
        public static readonly string Type = typeof(System.Guid).FullName;

        /// <summary>
        /// Contains metadata about the 'B' format for the Guid Primitively type.
        /// </summary>
        public readonly struct B
        {
            public const string Example = "{2c48c152-7cb7-4f51-8f01-704454f36e60}";
            public const string Format = "B";
            public const int Length = 38;
        }

        /// <summary>
        /// Contains metadata about the 'D' format for the Guid Primitively type.
        /// </summary>
        public readonly struct D
        {
            public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
            public const string Format = "D";
            public const int Length = 36;
        }

        /// <summary>
        /// Contains metadata about the 'N' format for the Guid Primitively type.
        /// </summary>
        public readonly struct N
        {
            public const string Example = "2c48c1527cb74f518f01704454f36e60";
            public const string Format = "N";
            public const int Length = 32;
        }

        /// <summary>
        /// Contains metadata about the 'P' format for the Guid Primitively type.
        /// </summary>
        public readonly struct P
        {
            public const string Example = "(2c48c152-7cb7-4f51-8f01-704454f36e60)";
            public const string Format = "P";
            public const int Length = 38;
        }

        /// <summary>
        /// Contains metadata about the 'X' format for the Guid Primitively type.
        /// </summary>
        public readonly struct X
        {
            public const string Example = "{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}";
            public const string Format = "X";
            public const int Length = 68;
        }
    }

    /// <summary>
    /// Contains metadata about various Numeric Primitively types.
    /// </summary>
    public readonly struct Numeric
    {
        /// <summary>
        /// Contains metadata about the Byte Primitively type.
        /// </summary>
        public readonly struct Byte
        {
            public static readonly string Example = $"{byte.MaxValue / 2}";
            public static readonly string Interface = typeof(IByte).FullName;
            public static readonly string JsonReaderMethod = "TryGetByte";
            public static readonly decimal Maximum = byte.MaxValue;
            public static readonly decimal Minimum = byte.MinValue;
            public static readonly string Type = typeof(byte).FullName;
        }

        /// <summary>
        /// Contains metadata about the Int Primitively type.
        /// </summary>
        public readonly struct Int
        {
            public static readonly string Example = $"{int.MaxValue / 2}";
            public static readonly string Interface = typeof(IInt).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt32";
            public static readonly decimal Maximum = int.MaxValue;
            public static readonly decimal Minimum = int.MinValue;
            public static readonly string Type = typeof(int).FullName;
        }

        /// <summary>
        /// Contains metadata about the Long Primitively type.
        /// </summary>
        public readonly struct Long
        {
            public static readonly string Example = $"{long.MaxValue / 2}";
            public static readonly string Interface = typeof(ILong).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt64";
            public static readonly decimal Maximum = long.MaxValue;
            public static readonly decimal Minimum = long.MinValue;
            public static readonly string Type = typeof(long).FullName;
        }

        /// <summary>
        /// Contains metadata about the SByte Primitively type.
        /// </summary>
        public readonly struct SByte
        {
            public static readonly string Example = $"{sbyte.MaxValue / 2}";
            public static readonly string Interface = typeof(ISByte).FullName;
            public static readonly string JsonReaderMethod = "TryGetSByte";
            public static readonly decimal Maximum = sbyte.MaxValue;
            public static readonly decimal Minimum = sbyte.MinValue;
            public static readonly string Type = typeof(sbyte).FullName;
        }

        /// <summary>
        /// Contains metadata about the Short Primitively type.
        /// </summary>
        public readonly struct Short
        {
            public static readonly string Example = $"{short.MaxValue / 2}";
            public static readonly string Interface = typeof(IShort).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt16";
            public static readonly decimal Maximum = short.MaxValue;
            public static readonly decimal Minimum = short.MinValue;
            public static readonly string Type = typeof(short).FullName;
        }

        /// <summary>
        /// Contains metadata about the UInt Primitively type.
        /// </summary>
        public readonly struct UInt
        {
            public static readonly string Example = $"{uint.MaxValue / 2}";
            public static readonly string Interface = typeof(IUInt).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt32";
            public static readonly decimal Maximum = uint.MaxValue;
            public static readonly decimal Minimum = uint.MinValue;
            public static readonly string Type = typeof(uint).FullName;
        }

        /// <summary>
        /// Contains metadata about the ULong Primitively type.
        /// </summary>
        public readonly struct ULong
        {
            public static readonly string Example = $"{ulong.MaxValue / 2}";
            public static readonly string Interface = typeof(IULong).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt64";
            public static readonly decimal Maximum = ulong.MaxValue;
            public static readonly decimal Minimum = ulong.MinValue;
            public static readonly string Type = typeof(ulong).FullName;
        }

        /// <summary>
        /// Contains metadata about the UShort Primitively type.
        /// </summary>
        public readonly struct UShort
        {
            public static readonly string Example = $"{ushort.MaxValue / 2}";
            public static readonly string Interface = typeof(IUShort).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt16";
            public static readonly decimal Maximum = ushort.MaxValue;
            public static readonly decimal Minimum = ushort.MinValue;
            public static readonly string Type = typeof(ushort).FullName;
        }

        /// <summary>
        /// Contains metadata about the Single (float) Primitively type.
        /// </summary>
        public readonly struct Single
        {
            public static readonly string Example = $"{0.60344}";
            public static readonly string Interface = typeof(ISingle).FullName;
            public static readonly string JsonReaderMethod = "TryGetSingle";
            public static readonly float Maximum = float.MaxValue;
            public static readonly float Minimum = float.MinValue;
            public static readonly string Type = typeof(float).FullName;
        }

        /// <summary>
        /// Contains metadata about the Double Primitively type.
        /// </summary>
        public readonly struct Double
        {
            public static readonly string Example = $"{0.60344}";
            public static readonly string Interface = typeof(IDouble).FullName;
            public static readonly string JsonReaderMethod = "TryGetDouble";
            public static readonly double Maximum = double.MaxValue;
            public static readonly double Minimum = double.MinValue;
            public static readonly string Type = typeof(double).FullName;
            public static readonly int Digits = -1;
            public static readonly MidpointRounding Mode = MidpointRounding.ToEven;
        }
    }
}
