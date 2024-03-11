using System.ComponentModel.DataAnnotations;

namespace Primitively;

/// <summary>
/// This is an abstract class for custom attributes that can be applied to struct types.
/// It provides a flag to control whether to generate a method that implements the <see cref="IValidatableObject"/> interface.
/// </summary>
[AttributeUsage(AttributeTargets.Struct, Inherited = true, AllowMultiple = false)]
public abstract class PrimitiveAttribute : Attribute
{
    /// <summary>
    /// Gets or sets a flag to indicate whether to source generate a <see cref="IValidatableObject.Validate(ValidationContext)"/> method.
    /// </summary>
    /// <remarks>
    /// For more information about validation: <see href="https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0#ivalidatableobject"/>
    /// </remarks>
    /// <value>
    /// When set to true, it will output a Primitively type that implements the <see cref="IValidatableObject"/> interface.
    /// </value>
    public bool ImplementIValidatableObject { get; set; } = true;
}
