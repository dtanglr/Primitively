namespace Primitively;

internal readonly struct MetaData
{
    public readonly struct DateOnly
    {
        public static readonly string Interface = typeof(IDateOnly).FullName;
        public static readonly string Type = typeof(System.DateTime).FullName;

        public readonly struct Iso8601
        {
            public const string Example = "2022-12-31";
            public const string Format = "yyyy-MM-dd";
            public const int Length = 10;
        }
    }

    public readonly struct Guid
    {
        public static readonly string Interface = typeof(IGuid).FullName;
        public static readonly string Type = typeof(System.Guid).FullName;

        public readonly struct B
        {
            public const string Example = "{2c48c152-7cb7-4f51-8f01-704454f36e60}";
            public const string Format = "B";
            public const int Length = 38;
        }

        public readonly struct D
        {
            public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
            public const string Format = "D";
            public const int Length = 36;
        }

        public readonly struct N
        {
            public const string Example = "2c48c1527cb74f518f01704454f36e60";
            public const string Format = "N";
            public const int Length = 32;
        }
        public readonly struct P
        {
            public const string Example = "(2c48c152-7cb7-4f51-8f01-704454f36e60)";
            public const string Format = "P";
            public const int Length = 38;
        }

        public readonly struct X
        {
            public const string Example = "{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}";
            public const string Format = "X";
            public const int Length = 68;
        }
    }

    public readonly struct Integer
    {
        public readonly struct Byte
        {
            public static readonly string Example = $"{byte.MaxValue / 2}";
            public static readonly string Interface = typeof(IByte).FullName;
            public static readonly string JsonReaderMethod = "TryGetByte";
            public static readonly decimal Maximum = byte.MaxValue;
            public static readonly decimal Minimum = byte.MinValue;
            public static readonly string Type = typeof(byte).FullName;
        }

        public readonly struct Int
        {
            public static readonly string Example = $"{int.MaxValue / 2}";
            public static readonly string Interface = typeof(IInt).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt32";
            public static readonly decimal Maximum = int.MaxValue;
            public static readonly decimal Minimum = int.MinValue;
            public static readonly string Type = typeof(int).FullName;
        }

        public readonly struct Long
        {
            public static readonly string Example = $"{long.MaxValue / 2}";
            public static readonly string Interface = typeof(ILong).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt64";
            public static readonly decimal Maximum = long.MaxValue;
            public static readonly decimal Minimum = long.MinValue;
            public static readonly string Type = typeof(long).FullName;
        }

        public readonly struct SByte
        {
            public static readonly string Example = $"{sbyte.MaxValue / 2}";
            public static readonly string Interface = typeof(ISByte).FullName;
            public static readonly string JsonReaderMethod = "TryGetSByte";
            public static readonly decimal Maximum = sbyte.MaxValue;
            public static readonly decimal Minimum = sbyte.MinValue;
            public static readonly string Type = typeof(sbyte).FullName;
        }

        public readonly struct Short
        {
            public static readonly string Example = $"{short.MaxValue / 2}";
            public static readonly string Interface = typeof(IShort).FullName;
            public static readonly string JsonReaderMethod = "TryGetInt16";
            public static readonly decimal Maximum = short.MaxValue;
            public static readonly decimal Minimum = short.MinValue;
            public static readonly string Type = typeof(short).FullName;
        }

        public readonly struct UInt
        {
            public static readonly string Example = $"{uint.MaxValue / 2}";
            public static readonly string Interface = typeof(IUInt).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt32";
            public static readonly decimal Maximum = uint.MaxValue;
            public static readonly decimal Minimum = uint.MinValue;
            public static readonly string Type = typeof(uint).FullName;
        }

        public readonly struct ULong
        {
            public static readonly string Example = $"{ulong.MaxValue / 2}";
            public static readonly string Interface = typeof(IULong).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt64";
            public static readonly decimal Maximum = ulong.MaxValue;
            public static readonly decimal Minimum = ulong.MinValue;
            public static readonly string Type = typeof(ulong).FullName;
        }

        public readonly struct UShort
        {
            public static readonly string Example = $"{ushort.MaxValue / 2}";
            public static readonly string Interface = typeof(IUShort).FullName;
            public static readonly string JsonReaderMethod = "TryGetUInt16";
            public static readonly decimal Maximum = ushort.MaxValue;
            public static readonly decimal Minimum = ushort.MinValue;
            public static readonly string Type = typeof(ushort).FullName;
        }
    }
}
