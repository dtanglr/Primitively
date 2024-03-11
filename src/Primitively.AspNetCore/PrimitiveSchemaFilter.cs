using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

/// <summary>
/// This class is a custom schema filter for Primitively types.
/// </summary>
public class PrimitiveSchemaFilter : ISchemaFilter
{
    private readonly Func<IEnumerable<PrimitiveInfo>> _primitiveInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveSchemaFilter"/> class.
    /// </summary>
    /// <param name="primitiveInfo">A function that retrieves a collection of Primitively types.</param>
    /// <exception cref="ArgumentNullException">Thrown when the primitiveInfo parameter is null.</exception>
    public PrimitiveSchemaFilter(Func<IEnumerable<PrimitiveInfo>> primitiveInfo)
    {
        _primitiveInfo = primitiveInfo ?? throw new ArgumentNullException(nameof(primitiveInfo));
    }

    /// <summary>
    /// Applies the schema filter to a specific schema.
    /// </summary>
    /// <param name="schema">The schema to apply the filter to.</param>
    /// <param name="context">The schema filter context.</param>
    /// <remarks>
    /// This method modifies the schema based on the type of the Primitively type. It sets the type, properties, example, format, and other properties of the schema.
    /// </remarks>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        if (type is null || !type.IsValueType || !type.IsAssignableTo(typeof(IPrimitive)))
        {
            return;
        }

        var items = _primitiveInfo.Invoke();
        var item = items.SingleOrDefault(t => t.Type.Equals(type));

        switch (item)
        {
            case DateOnlyInfo dateOnlyInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(item.Example);
                    schema.Format = "date";
                    schema.MinLength = dateOnlyInfo.Length;
                    schema.MaxLength = dateOnlyInfo.Length;

                    break;
                }

            case GuidInfo guidInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(item.Example);
                    schema.Format = "uuid";
                    schema.MinLength = guidInfo.Length;
                    schema.MaxLength = guidInfo.Length;

                    break;
                }

            case StringInfo stringInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(item.Example);
                    schema.Format = stringInfo.Format;
                    schema.MinLength = stringInfo.MinLength;
                    schema.MaxLength = stringInfo.MaxLength;
                    schema.Pattern = stringInfo.Pattern;

                    break;
                }

            case IntegerInfo integerInfo:
                {
                    schema.Type = "integer";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(item.Example);
                    schema.Minimum = integerInfo.Minimum;
                    schema.Maximum = integerInfo.Maximum;
                    schema.Format = integerInfo.DataType switch
                    {
                        DataType.Int => "int32",
                        DataType.Long => "int64",
                        _ => null
                    };

                    break;
                }

            default:
                break;
        }
    }
}
