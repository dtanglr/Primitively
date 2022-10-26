namespace Primitively.IntegrationTests.Types.DateOnlyTests.BirthDateTests;

public class JsonConverterTests : JsonConverterTests<BirthDate.JsonConverter, BirthDate>
{
    protected override BirthDate PrimitiveWithValue => (BirthDate)BirthDate.Example;
}
