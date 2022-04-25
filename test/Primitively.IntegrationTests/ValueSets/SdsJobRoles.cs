using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Primitively.IntegrationTests.ValueSets;

public partial class SdsJobRoles : ReadOnlyCollection<SdsJobRole>
{
    private static readonly Lazy<List<SdsJobRole>> _list = new(() => new List<SdsJobRole>(GetValues()), LazyThreadSafetyMode.PublicationOnly);

    public SdsJobRoles() : base(_list.Value) { }

    public static SdsJobRoles Instance => new();

    public SdsJobRole this[SdsJobRoleCode code] => Items.Single(p => p.Code.Equals(code));

    public SdsJobRole Get(SdsJobRoleCode code) => Items.Single(p => p.Code.Equals(code));

    public bool TryGet(SdsJobRoleCode code, out SdsJobRole item) => (item = code.HasValue ? Items.SingleOrDefault(p => p.Code.Equals(code)) : default) != default;

    public bool Exists(SdsJobRoleCode code) => Items.Any(p => p.Code.Equals(code));

    private static IEnumerable<SdsJobRole> GetValues()
    {
        yield return SdsJobRole.R8000;
        yield return SdsJobRole.R8001;
        yield return SdsJobRole.R8002;
        yield return SdsJobRole.R8003;
        yield return SdsJobRole.R8004;
        yield return SdsJobRole.R8005;
        yield return SdsJobRole.R8006;
        yield return SdsJobRole.R8007;
        yield return SdsJobRole.R8008;
        yield return SdsJobRole.R8009;
        yield return SdsJobRole.R8010;
        yield return SdsJobRole.R8011;
        yield return SdsJobRole.R8012;
        yield return SdsJobRole.R8013;
        yield return SdsJobRole.R8014;
        yield return SdsJobRole.R8015;
        yield return SdsJobRole.R8016;
        yield return SdsJobRole.R8017;
        yield return SdsJobRole.R8024;
    }
}
