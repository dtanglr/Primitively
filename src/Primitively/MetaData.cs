namespace Primitively;

internal struct MetaData
{
    public const string ConditionalCompilationSymbol = "PRIMITIVELY_USAGES";

    public struct DateOnly
    {
        public struct Iso8601
        {
            public const int Length = 10;
            public const string Example = "2022-12-31";
            public const string Format = "yyyy-MM-dd";
        }
    }

    public struct Guid
    {
        public struct N
        {
            public const int Length = 32;
            public const string Example = "2c48c1527cb74f518f01704454f36e60";
            public const string Format = "N";
        }

        public struct D
        {
            public const int Length = 36;
            public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
            public const string Format = "D";
        }

        public struct B
        {
            public const int Length = 38;
            public const string Example = "{2c48c152-7cb7-4f51-8f01-704454f36e60}";
            public const string Format = "B";
        }

        public struct P
        {
            public const int Length = 38;
            public const string Example = "(2c48c152-7cb7-4f51-8f01-704454f36e60)";
            public const string Format = "P";
        }

        public struct X
        {
            public const int Length = 68;
            public const string Example = "{0x2c48c152,0x7cb7,0x4f51,{0x8f,0x01,0x70,0x44,0x54,0xf3,0x6e,0x60}}";
            public const string Format = "X";
        }
    }
}
