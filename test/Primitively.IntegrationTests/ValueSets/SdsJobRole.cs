using System;

namespace Primitively.IntegrationTests.ValueSets;

public readonly partial record struct SdsJobRole(SdsJobRoleCode Code, string Display, string System) : IPrimitiveCode<SdsJobRoleCode>, IEquatable<SdsJobRole>
{
    public bool Equals(SdsJobRole other) => Code == other.Code;

    public override int GetHashCode() => Code.GetHashCode();

    public static implicit operator SdsJobRoleCode(SdsJobRole value) => value.Code;
    public static explicit operator SdsJobRole(SdsJobRoleCode code) => SdsJobRoles.Instance.Get(code);

    public const string SystemUrl = "https://fhir.hl7.org.uk/CodeSystem/UKCore-SDSJobRoleName";

    public static readonly SdsJobRole R8000 = new((SdsJobRoleCode)"R8000", "Clinical Practitioner Access Role", SystemUrl);
    public static readonly SdsJobRole R8001 = new((SdsJobRoleCode)"R8001", "Nurse Access Role", SystemUrl);
    public static readonly SdsJobRole R8002 = new((SdsJobRoleCode)"R8002", "Nurse Manager Access Role", SystemUrl);
    public static readonly SdsJobRole R8003 = new((SdsJobRoleCode)"R8003", "Health Professional Access Role", SystemUrl);
    public static readonly SdsJobRole R8004 = new((SdsJobRoleCode)"R8004", "Healthcare Student Access Role", SystemUrl);
    public static readonly SdsJobRole R8005 = new((SdsJobRoleCode)"R8005", "Biomedical Scientist Access Role", SystemUrl);
    public static readonly SdsJobRole R8006 = new((SdsJobRoleCode)"R8006", "Medical Secretary Access Role", SystemUrl);
    public static readonly SdsJobRole R8007 = new((SdsJobRoleCode)"R8007", "Clinical Coder Access Role", SystemUrl);
    public static readonly SdsJobRole R8008 = new((SdsJobRoleCode)"R8008", "Admin/Clinical Support Access Role", SystemUrl);
    public static readonly SdsJobRole R8009 = new((SdsJobRoleCode)"R8009", "Receptionist Access Role", SystemUrl);
    public static readonly SdsJobRole R8010 = new((SdsJobRoleCode)"R8010", "Clerical Access Role", SystemUrl);
    public static readonly SdsJobRole R8011 = new((SdsJobRoleCode)"R8011", "Clerical Manager Access Role", SystemUrl);
    public static readonly SdsJobRole R8012 = new((SdsJobRoleCode)"R8012", "Information Officer Access Role", SystemUrl);
    public static readonly SdsJobRole R8013 = new((SdsJobRoleCode)"R8013", "Health Records Manager Access Role", SystemUrl);
    public static readonly SdsJobRole R8014 = new((SdsJobRoleCode)"R8014", "Social Worker Access Role", SystemUrl);
    public static readonly SdsJobRole R8015 = new((SdsJobRoleCode)"R8015", "Systems Support Access Role", SystemUrl);
    public static readonly SdsJobRole R8016 = new((SdsJobRoleCode)"R8016", "Midwife Access Role", SystemUrl);
    public static readonly SdsJobRole R8017 = new((SdsJobRoleCode)"R8017", "Midwife Manager Access Role", SystemUrl);
    public static readonly SdsJobRole R8024 = new((SdsJobRoleCode)"R8024", "Bank Access Role", SystemUrl);
}
