namespace Primitively.IntegrationTests.NumericTests.Single;

public class JsonConverterTests : PrimitiveJsonConverterTests<SingleId.JsonConverter, SingleId>
{
    protected override SingleId PrimitiveWithValue => (SingleId)SingleId.Example;
}
