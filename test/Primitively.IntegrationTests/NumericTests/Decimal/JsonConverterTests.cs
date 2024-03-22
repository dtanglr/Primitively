namespace Primitively.IntegrationTests.NumericTests.Decimal;

public class JsonConverterTests : PrimitiveJsonConverterTests<DecimalId.JsonConverter, DecimalId>
{
    protected override DecimalId PrimitiveWithValue => (DecimalId)DecimalId.Example;
}
