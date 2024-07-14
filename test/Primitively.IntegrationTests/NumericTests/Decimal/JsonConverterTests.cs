namespace Primitively.IntegrationTests.NumericTests.Decimal;

public class JsonConverterTests : PrimitiveJsonConverterTests<DecimalWith2Digits.JsonConverter, DecimalWith2Digits>
{
    protected override DecimalWith2Digits PrimitiveWithValue => (DecimalWith2Digits)DecimalWith2Digits.Example;
}
