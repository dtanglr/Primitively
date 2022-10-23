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
}
