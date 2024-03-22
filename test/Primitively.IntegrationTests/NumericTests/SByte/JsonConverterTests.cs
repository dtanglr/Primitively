namespace Primitively.IntegrationTests.NumericTests.SByte;

public class JsonConverterTests : PrimitiveJsonConverterTests<SByteId.JsonConverter, SByteId>
{
    protected override SByteId PrimitiveWithValue => (SByteId)SByteId.Example;
}
