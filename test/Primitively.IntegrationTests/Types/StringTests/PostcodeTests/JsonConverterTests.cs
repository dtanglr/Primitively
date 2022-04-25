namespace Primitively.IntegrationTests.Types.StringTests.PostcodeTests;

public class JsonConverterTests : JsonConverterTests<PostcodeJsonConverter, Postcode, string>
{
    protected override Postcode PrimitiveWithValue => Postcode.Parse("DN551PT");
}
