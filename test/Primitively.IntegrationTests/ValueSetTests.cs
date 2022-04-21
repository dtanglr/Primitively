using Primitively.IntegrationTests.ValueSets;
using Xunit;

namespace Primitively.IntegrationTests;

public class ValueSetTests
{
    [Fact]
    public void LazyLoadsCorrectNumberOfValueSets()
    {
        var values = ValueSet1.ValueSetCollection.Get();
        Assert.Equal(6, values.Count);
    }

    [Fact]
    public void LazyLoadsDataOnce()
    {
        var values = ValueSet1.ValueSetCollection.Get();
        var values2 = ValueSet1.ValueSetCollection.Get();
        Assert.Equal(6, values2.Count);
    }
}
