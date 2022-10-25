namespace Primitively.IntegrationTests.Types.StringTests.SevenDigitsTests;

public class JsonConverterTests : JsonConverterTests<SevenDigitsJsonConverter, SevenDigits>
{
    protected override SevenDigits PrimitiveWithValue => (SevenDigits)SevenDigits.Example;
}
