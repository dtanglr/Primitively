﻿using Primitively;

namespace Acme.Examples;

[String(4, 8, Example = "N20 1LP", Pattern = "^[A-Z]{1,2}[0-9][A-Z0-9]? ?[0-9][A-Z]{2}$")]
public partial record struct Postcode;
