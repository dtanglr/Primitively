
    public global::System.Collections.Generic.IEnumerable<global::System.ComponentModel.DataAnnotations.ValidationResult> Validate(global::System.ComponentModel.DataAnnotations.ValidationContext validationContext)
    {
        if (!HasValue)
        {
            yield return new global::System.ComponentModel.DataAnnotations.ValidationResult($"{validationContext.DisplayName} must have a valid value.");
        }
    }
