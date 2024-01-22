using System.ComponentModel.DataAnnotations;

namespace Primitively;

/// <summary>
/// The interface that is implemented by all the Primitively attributes that invoke source code generation
/// </summary>
public interface IPimitivelyAttribute
{
    /// <summary>
    /// A flag to indicate whether to source generate a <see cref="IValidatableObject.Validate(ValidationContext)"/> method.
    /// </summary>
    /// <remarks>
    /// For more information about validation: <see href="https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0#ivalidatableobject"/>
    /// </remarks>
    /// <value>
    /// When set to true; will output a Primitively type that implements the <see cref="IValidatableObject"/> interface
    /// </value>
    bool ImplementIValidatableObject { get; set; }
}
