namespace Primitively.IntegrationTests.Types.StringTests;

public class JsonConverterTests : PrimitiveJsonConverterTests<SevenDigits.JsonConverter, SevenDigits>
{
    protected override SevenDigits PrimitiveWithValue => (SevenDigits)SevenDigits.Example;
}
