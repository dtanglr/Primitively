using System.ComponentModel.DataAnnotations;

namespace Primitively;

[AttributeUsage(AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public abstract class PrimitiveAttribute : Attribute
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
    public bool ImplementIValidatableObject { get; set; } = true;
}
