namespace Primitively.IntegrationTests.IntegerTests.Short;

public class JsonConverterTests : PrimitiveJsonConverterTests<ShortId.JsonConverter, ShortId>
{
    protected override ShortId PrimitiveWithValue => (ShortId)ShortId.Example;
}
