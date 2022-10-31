namespace Primitively.IntegrationTests.Types.GuidTests;

public class JsonConverterTests : PrimitiveJsonConverterTests<CorrelationId.JsonConverter, CorrelationId>
{
    protected override CorrelationId PrimitiveWithValue => (CorrelationId)CorrelationId.Example;
}
