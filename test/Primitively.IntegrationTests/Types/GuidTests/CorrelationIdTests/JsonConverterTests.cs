namespace Primitively.IntegrationTests.Types.GuidTests.CorrelationIdTests;

public class JsonConverterTests : JsonConverterTests<CorrelationId.JsonConverter, CorrelationId>
{
    protected override CorrelationId PrimitiveWithValue => (CorrelationId)CorrelationId.Example;
}
