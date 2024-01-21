using System.ComponentModel.DataAnnotations;

namespace Primitively;

/// <summary>
/// The interface that is implemented by all the Primitively attributes that invoke source code generation
/// </summary>
public interface IPimitivelyAttribute
{
    /// <summary>
    /// When set to true; will output a Primitively type that implements the <see cref="IValidatableObject"/>
    /// </summary>
    /// <remarks>
    /// For more information about validation: <see href="https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0#ivalidatableobject"/>
    /// </remarks>
    bool ImplementIValidatableObject { get; set; }
}
