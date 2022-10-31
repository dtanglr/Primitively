namespace Primitively.IntegrationTests.GuidTests;

public class JsonConverterTests : PrimitiveJsonConverterTests<CorrelationId.JsonConverter, CorrelationId>
{
    protected override CorrelationId PrimitiveWithValue => (CorrelationId)CorrelationId.Example;
}
