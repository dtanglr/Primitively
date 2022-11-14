namespace Primitively.IntegrationTests.IntegerTests.SByte;

public class JsonConverterTests : PrimitiveJsonConverterTests<SByteId.JsonConverter, SByteId>
{
    protected override SByteId PrimitiveWithValue => (SByteId)SByteId.Example;
}
