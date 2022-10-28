namespace Primitively.IntegrationTests.Types.StringTests;

public class JsonConverterTests : JsonConverterTests<SevenDigits.JsonConverter, SevenDigits>
{
    protected override SevenDigits PrimitiveWithValue => (SevenDigits)SevenDigits.Example;
}
