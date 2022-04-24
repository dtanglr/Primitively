using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Primitively.IntegrationTests.ValueSets;

[StringPrimitive(@"^R\d{4}$", "R8000", length: 5)]
public readonly partial record struct SdsJobRoleCode;

public readonly partial record struct SdsJobRole(SdsJobRoleCode Code, string Display) : IPrimitiveCode<SdsJobRoleCode>, IEquatable<SdsJobRole>
{
    public const string SystemUrl = "https://fhir.hl7.org.uk/CodeSystem/UKCore-SDSJobRoleName";

    public bool Equals(SdsJobRole other) => Code == other.Code;

    public override int GetHashCode() => Code.GetHashCode();
}

public partial class SdsJobRoles : ReadOnlyCollection<SdsJobRole>
{
    private static readonly Lazy<List<SdsJobRole>> _list = new(() => new List<SdsJobRole>(GetValues()), LazyThreadSafetyMode.PublicationOnly);

    public SdsJobRoles() : base(_list.Value) { }

    public SdsJobRole this[SdsJobRoleCode code] => Items.Single(p => p.Code.Equals(code));

    public SdsJobRole Get(SdsJobRoleCode code) => Items.Single(p => p.Code.Equals(code));

    public bool TryGet(SdsJobRoleCode code, out SdsJobRole item) => (item = code.HasValue ? Items.SingleOrDefault(p => p.Code.Equals(code)) : default) != default;

    public bool Exists(SdsJobRoleCode code) => Items.Any(p => p.Code.Equals(code));

    private static partial IEnumerable<SdsJobRole> GetValues();
}

public partial class SdsJobRoles
{
    private static partial IEnumerable<SdsJobRole> GetValues()
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

public readonly partial record struct SdsJobRole
{
    public static readonly SdsJobRole R8000 = new((SdsJobRoleCode)"R8000", "Clinical Practitioner Access Role");
    public static readonly SdsJobRole R8001 = new((SdsJobRoleCode)"R8001", "Nurse Access Role");
    public static readonly SdsJobRole R8002 = new((SdsJobRoleCode)"R8002", "Nurse Manager Access Role");
    public static readonly SdsJobRole R8003 = new((SdsJobRoleCode)"R8003", "Health Professional Access Role");
    public static readonly SdsJobRole R8004 = new((SdsJobRoleCode)"R8004", "Healthcare Student Access Role");
    public static readonly SdsJobRole R8005 = new((SdsJobRoleCode)"R8005", "Biomedical Scientist Access Role");
    public static readonly SdsJobRole R8006 = new((SdsJobRoleCode)"R8006", "Medical Secretary Access Role");
    public static readonly SdsJobRole R8007 = new((SdsJobRoleCode)"R8007", "Clinical Coder Access Role");
    public static readonly SdsJobRole R8008 = new((SdsJobRoleCode)"R8008", "Admin/Clinical Support Access Role");
    public static readonly SdsJobRole R8009 = new((SdsJobRoleCode)"R8009", "Receptionist Access Role");
    public static readonly SdsJobRole R8010 = new((SdsJobRoleCode)"R8010", "Clerical Access Role");
    public static readonly SdsJobRole R8011 = new((SdsJobRoleCode)"R8011", "Clerical Manager Access Role");
    public static readonly SdsJobRole R8012 = new((SdsJobRoleCode)"R8012", "Information Officer Access Role");
    public static readonly SdsJobRole R8013 = new((SdsJobRoleCode)"R8013", "Health Records Manager Access Role");
    public static readonly SdsJobRole R8014 = new((SdsJobRoleCode)"R8014", "Social Worker Access Role");
    public static readonly SdsJobRole R8015 = new((SdsJobRoleCode)"R8015", "Systems Support Access Role");
    public static readonly SdsJobRole R8016 = new((SdsJobRoleCode)"R8016", "Midwife Access Role");
    public static readonly SdsJobRole R8017 = new((SdsJobRoleCode)"R8017", "Midwife Manager Access Role");
    public static readonly SdsJobRole R8024 = new((SdsJobRoleCode)"R8024", "Bank Access Role");
}
