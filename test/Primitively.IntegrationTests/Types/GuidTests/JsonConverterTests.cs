namespace Primitively.IntegrationTests.Types.GuidTests;

public class JsonConverterTests : JsonConverterTests<CorrelationId.JsonConverter, CorrelationId>
{
    protected override CorrelationId PrimitiveWithValue => (CorrelationId)CorrelationId.Example;
}
