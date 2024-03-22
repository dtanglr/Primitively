namespace Primitively.IntegrationTests.NumericTests.ULong;

public class JsonConverterTests : PrimitiveJsonConverterTests<ULongId.JsonConverter, ULongId>
{
    protected override ULongId PrimitiveWithValue => (ULongId)ULongId.Example;
}
