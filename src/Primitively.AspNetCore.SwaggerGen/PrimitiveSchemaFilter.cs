using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore.SwaggerGen;

/// <summary>
/// The PrimitiveSchemaFilter class is a custom schema filter for Primitively types.
/// </summary>
public class PrimitiveSchemaFilter : ISchemaFilter
{
    private readonly PrimitiveRegistry _registry;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveSchemaFilter"/> class.
    /// </summary>
    /// <param name="registry">The registry of Primitively types.</param>
    public PrimitiveSchemaFilter(PrimitiveRegistry registry)
    {
        _registry = registry;
    }

    /// <summary>
    /// Applies the schema filter to a specific schema.
    /// </summary>
    /// <param name="schema">The schema to apply the filter to.</param>
    /// <param name="context">The schema filter context.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        if (type is null || !type.IsValueType || !type.IsAssignableTo(typeof(IPrimitive)) || !_registry.TryGet(type, out var primitiveInfo))
        {
            return;
        }

        switch (primitiveInfo)
        {
            case DateOnlyInfo dateOnlyInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(primitiveInfo.Example);
                    schema.Format = "date";
                    schema.MinLength = dateOnlyInfo.Length;
                    schema.MaxLength = dateOnlyInfo.Length;

                    break;
                }

            case GuidInfo guidInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(primitiveInfo.Example);
                    schema.Format = guidInfo.Specifier switch
                    {
                        Specifier.N => "string",
                        Specifier.X => "string",
                        _ => "uuid"
                    };
                    schema.MinLength = guidInfo.Length;
                    schema.MaxLength = guidInfo.Length;

                    break;
                }

            case StringInfo stringInfo:
                {
                    schema.Type = "string";
                    schema.Properties = null;
                    schema.Example = new OpenApiString(primitiveInfo.Example);
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
                    schema.Example = new OpenApiString(primitiveInfo.Example);
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
