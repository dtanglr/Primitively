using System;

namespace Primitively.IntegrationTests.Types.GuidTests.CorrelationIdTests;

public class JsonConverterTests : JsonConverterTests<CorrelationId.CorrelationIdJsonConverter, CorrelationId, Guid>
{
    protected override CorrelationId PrimitiveWithValue => CorrelationId.Parse("9BC12195-B4A9-4880-B526-A0BE96EDDA08");
}
