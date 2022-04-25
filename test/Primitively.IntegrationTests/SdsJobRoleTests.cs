using Primitively.IntegrationTests.ValueSets;
using Xunit;

namespace Primitively.IntegrationTests;

public class SdsJobRoleTests
{
    [Fact]
    public void LazyLoadsCorrectNumberOfValueSets()
    {
        var valueSet = new SdsJobRoles();
        Assert.Equal(19, valueSet.Count);
    }

    [Fact]
    public void LazyLoadsDataOnce()
    {
        var values = new SdsJobRoles();
        var values2 = new SdsJobRoles();

        Assert.Equal(19, values2.Count);
    }

    [Fact]
    public void ContainsValue()
    {
        var values = new SdsJobRoles();
        var value = SdsJobRole.R8000;

        Assert.Contains(value, values);
    }

    [Fact]
    public void CodeExists()
    {
        var values = new SdsJobRoles();
        var value = SdsJobRole.R8000;

        Assert.True(values.Exists(value.Code));
    }

    [Fact]
    public void GetsIndex()
    {
        var values = new SdsJobRoles();
        var value = SdsJobRole.R8000;

        Assert.Equal(value, values[value.Code]);
    }

    [Fact]
    public void GetsValue()
    {
        var values = new SdsJobRoles();
        var value = SdsJobRole.R8000;

        Assert.Equal(value, values.Get(value.Code));
    }

    [Fact]
    public void TryGetsValue()
    {
        var values = new SdsJobRoles();
        var value = SdsJobRole.R8000;

        Assert.True(values.TryGet(value.Code, out var result));
        Assert.Equal(value, result);
    }

    [Fact]
    public void SwitchExperiement()
    {
        var value = SdsJobRole.R8000;
        var result = value switch
        {
            var (code, _, _) when code == value.Code => SdsJobRole.R8000,
            _ when value == SdsJobRole.R8000 => SdsJobRole.R8000,
            _ => SdsJobRole.R8001
        };

        Assert.Equal(value, result);
    }

    [Fact]
    public void CanConvertToSdsRoleCodeFromSdsRoleImplicitly()
    {
        var value = SdsJobRole.R8000;
        var code = value.Code;

        Assert.Equal(code, value.Code);
        Assert.Equal(code, value);
    }

    [Fact]
    public void CanConvertToSdsRoleFromSdsRoleCodeExplicitly()
    {
        // Assign
        var codeAsString = "R8000";
        var code = SdsJobRoleCode.Parse(codeAsString); // Casting as works now e.g. (SdsJobRoleCode)codeAsString;
        var expected = SdsJobRole.R8000;

        // Act
        var converted = (SdsJobRole)code; // Cast from string primitive to object containing all Fhir properties

        Assert.Equal(expected.Code, code);
        Assert.Equal(expected, converted);
        Assert.Equal(expected.Code, converted.Code); // "R8000"
        Assert.Equal(expected.Display, converted.Display); // "Clinical Practitioner Access Role"
        Assert.Equal(expected.System, converted.System); // "https://fhir.hl7.org.uk/CodeSystem/UKCore-SDSJobRoleName"
    }
}
