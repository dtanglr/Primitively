
    public System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
    {
        if (!HasValue)
        {
            yield return new System.ComponentModel.DataAnnotations.ValidationResult($"{validationContext.DisplayName} must have a valid value.");
        }
    }
