namespace Primitively.IntegrationTests.NumericTests.Int;

public class JsonConverterTests : PrimitiveJsonConverterTests<IntId.JsonConverter, IntId>
{
    protected override IntId PrimitiveWithValue => (IntId)IntId.Example;
}
