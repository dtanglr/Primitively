namespace Primitively.IntegrationTests.NumericTests.Short;

public class JsonConverterTests : PrimitiveJsonConverterTests<ShortId.JsonConverter, ShortId>
{
    protected override ShortId PrimitiveWithValue => (ShortId)ShortId.Example;
}
