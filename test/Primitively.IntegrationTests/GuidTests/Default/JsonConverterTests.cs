namespace Primitively.IntegrationTests.GuidTests.Default;

public class JsonConverterTests : PrimitiveJsonConverterTests<DefaultThirtySixDigitsWithHyphens.JsonConverter, DefaultThirtySixDigitsWithHyphens>
{
    protected override DefaultThirtySixDigitsWithHyphens PrimitiveWithValue => (DefaultThirtySixDigitsWithHyphens)DefaultThirtySixDigitsWithHyphens.Example;
}
