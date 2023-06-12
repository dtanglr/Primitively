using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore;

public class PrimitiveSchemaFilter : ISchemaFilter
{
    private readonly Func<IEnumerable<PrimitiveInfo>> _primitiveInfo;

    public PrimitiveSchemaFilter(Func<IEnumerable<PrimitiveInfo>> primitiveInfo)
    {
        _primitiveInfo = primitiveInfo ?? throw new ArgumentNullException(nameof(primitiveInfo));
    }

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
