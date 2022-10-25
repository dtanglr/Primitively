using System;

namespace Primitively.IntegrationTests.Types.GuidTests.CorrelationIdTests;

public class JsonConverterTests : JsonConverterTests<CorrelationIdJsonConverter, CorrelationId>
{
    protected override CorrelationId PrimitiveWithValue => (CorrelationId)CorrelationId.Example;
}
