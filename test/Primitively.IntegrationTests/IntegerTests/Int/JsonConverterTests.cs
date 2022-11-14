namespace Primitively.IntegrationTests.IntegerTests.Int;

public class JsonConverterTests : PrimitiveJsonConverterTests<IntId.JsonConverter, IntId>
{
    protected override IntId PrimitiveWithValue => (IntId)IntId.Example;
}
