namespace Primitively.IntegrationTests.GuidTests.D;

public class JsonConverterTests : PrimitiveJsonConverterTests<ThirtySixDigitsWithHyphens.JsonConverter, ThirtySixDigitsWithHyphens>
{
    protected override ThirtySixDigitsWithHyphens PrimitiveWithValue => (ThirtySixDigitsWithHyphens)ThirtySixDigitsWithHyphens.Example;
}
