namespace Primitively.IntegrationTests.IntegerTests.UInt;

public class JsonConverterTests : PrimitiveJsonConverterTests<UIntId.JsonConverter, UIntId>
{
    protected override UIntId PrimitiveWithValue => (UIntId)UIntId.Example;
}
