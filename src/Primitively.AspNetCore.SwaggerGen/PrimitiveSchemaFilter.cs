using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore.SwaggerGen;

public class PrimitiveSchemaFilter(PrimitiveRegistry registry) : ISchemaFilter
{
    private readonly PrimitiveRegistry _registry = registry;

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
