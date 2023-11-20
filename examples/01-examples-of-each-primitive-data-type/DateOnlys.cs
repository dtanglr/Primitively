namespace Primitively.Examples;

// Currently a class library targeting netstandard2.0 can not support DateOnly.
// This is because System.DataOnly was introduced from net6.0
#if NET6_0_OR_GREATER

[DateOnly]
public partial record struct BirthDate;

[DateOnly]
public partial record struct DeathDate;

[DateOnly(ImplementIValidatableObject = true)]
public partial record struct ValidatableDate;

#endif
