namespace Primitively.IntegrationTests.NumericTests.UShort;

public class JsonConverterTests : PrimitiveJsonConverterTests<UShortId.JsonConverter, UShortId>
{
    protected override UShortId PrimitiveWithValue => (UShortId)UShortId.Example;
}
