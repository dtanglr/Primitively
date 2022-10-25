using Primitively;

namespace Examples;

[String(4, 8, Example = "M23 0LY", Pattern = "^[A-Z]{1,2}[0-9][A-Z0-9]? ?[0-9][A-Z]{2}$")]
public partial record struct Postcode;
