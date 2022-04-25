namespace Primitively.IntegrationTests.Types.StringTests.NhsNumberTests;

public class JsonConverterTests : JsonConverterTests<NhsNumber.NhsNumberJsonConverter, NhsNumber, string>
{
    protected override NhsNumber PrimitiveWithValue => NhsNumber.Parse("0123456789");
}
