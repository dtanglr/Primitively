namespace Primitively.IntegrationTests.GuidTests.N;

public class JsonConverterTests : PrimitiveJsonConverterTests<ThirtyTwoDigits.JsonConverter, ThirtyTwoDigits>
{
    protected override ThirtyTwoDigits PrimitiveWithValue => (ThirtyTwoDigits)ThirtyTwoDigits.Example;
}
