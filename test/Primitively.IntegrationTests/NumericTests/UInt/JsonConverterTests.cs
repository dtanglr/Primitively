namespace Primitively.IntegrationTests.NumericTests.UInt;

public class JsonConverterTests : PrimitiveJsonConverterTests<UIntId.JsonConverter, UIntId>
{
    protected override UIntId PrimitiveWithValue => (UIntId)UIntId.Example;
}
