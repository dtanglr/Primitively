namespace Primitively.IntegrationTests.NumericTests.Double;

public class JsonConverterTests : PrimitiveJsonConverterTests<DoubleId.JsonConverter, DoubleId>
{
    protected override DoubleId PrimitiveWithValue => (DoubleId)DoubleId.Example;
}
