namespace Primitively.IntegrationTests.NumericTests.Long;

public class JsonConverterTests : PrimitiveJsonConverterTests<LongId.JsonConverter, LongId>
{
    protected override LongId PrimitiveWithValue => (LongId)LongId.Example;
}
