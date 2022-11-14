namespace Primitively.IntegrationTests.IntegerTests.Byte;

public class JsonConverterTests : PrimitiveJsonConverterTests<ByteId.JsonConverter, ByteId>
{
    protected override ByteId PrimitiveWithValue => (ByteId)ByteId.Example;
}
