using System;
using Primitively.IntegrationTests.Types;
using Xunit;

namespace Primitively.IntegrationTests;

public class GuidTests
{
    [Fact]
    public void SameValuesAreEqual()
    {
        var id = Guid.NewGuid();
        var foo1 = new GuidId1(id);
        var foo2 = new GuidId1(id);

        Assert.Equal(foo1, foo2);
    }
}
