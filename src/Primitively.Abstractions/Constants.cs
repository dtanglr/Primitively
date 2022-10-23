namespace Primitively;

public class Constants
{
    public const string ConditionalCompilationSymbol = "PRIMITIVELY_USAGES";

    public struct DateOnly
    {
        public struct Iso8601
        {
            public const int Length = 10;
            public const string Example = "2022-01-01";
            public const string Format = "yyyy-MM-dd";
        }
    }

    public struct Guid
    {
        public struct Default
        {
            public const int Length = 36;
            public const string Example = "2c48c152-7cb7-4f51-8f01-704454f36e60";
            public const string Format = "D";
        }
    }

    public struct StringPrimitive
    {
        public struct NhsNumber
        {
            public const int Length = 10;
            public const string Example = "9000000009";
            public const string Pattern = "^[0-9]{10}$";
        }

        public struct OdsCode
        {
            public const int MinLength = 3;
            public const int MaxLength = 10;
            public const string Example = "Y123456";
            public const string Pattern = "^[A-Za-z0-9]{3,10}$";
        }

        public struct Postcode
        {
            public const int MinLength = 4;
            public const int MaxLength = 8;
            public const string Example = "PR7 6TE";
            public const string Pattern = "^[A-Z]{1,2}[0-9][A-Z0-9]? ?[0-9][A-Z]{2}$";
        }
    }
}
