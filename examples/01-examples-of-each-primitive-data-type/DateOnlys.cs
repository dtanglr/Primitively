namespace Primitively.Examples;

[DateOnly]
public partial record struct BirthDate;

[DateOnly]
public partial record struct DeathDate;

[DateOnly(ImplementIValidatableObject = true)]
public partial record struct ValidatableDate;
