namespace Primitively.IntegrationTests.Types.StringTests.SevenDigitsTests;

public class JsonConverterTests : JsonConverterTests<SevenDigitsJsonConverter, SevenDigits, string>
{
    protected override SevenDigits PrimitiveWithValue => (SevenDigits)SevenDigits.Example;
}
