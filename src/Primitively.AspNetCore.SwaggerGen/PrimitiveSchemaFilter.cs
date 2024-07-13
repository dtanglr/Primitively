using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Primitively.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Primitively.AspNetCore.SwaggerGen;

/// <summary>
/// This class is a custom schema filter for Primitively types.
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
    /// <remarks>
    /// This method modifies the schema based on the type of the Primitively type. It sets the type, properties, example, format, and other properties of the schema.
    /// </remarks>
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

            case INumericInfo numericInfo:
                {
                    schema.Type = numericInfo.DataType switch
                    {
                        DataType.Decimal => "number",
                        DataType.Double => "number",
                        DataType.Single => "number",
                        _ => "integer"
                    };
                    schema.Properties = null;
                    schema.Example = !string.IsNullOrWhiteSpace(numericInfo.Example) ? new OpenApiString(numericInfo.Example) : null;
                    schema.Minimum = numericInfo switch
                    {
                        NumericInfo<byte> byteInfo => byteInfo.Minimum,
                        NumericInfo<decimal> decimalInfo => decimalInfo.Minimum,
                        NumericInfo<double> doubleInfo => TryGetDecimal(doubleInfo.Minimum),
                        NumericInfo<int> intInfo => intInfo.Minimum,
                        NumericInfo<long> longInfo => longInfo.Minimum,
                        NumericInfo<sbyte> sbyteInfo => sbyteInfo.Minimum,
                        NumericInfo<short> shortInfo => shortInfo.Minimum,
                        NumericInfo<float> singleInfo => TryGetDecimal(singleInfo.Minimum),
                        NumericInfo<uint> uintInfo => uintInfo.Minimum,
                        NumericInfo<ulong> ulongInfo => ulongInfo.Minimum,
                        NumericInfo<ushort> ushortInfo => ushortInfo.Minimum,
                        _ => throw new NotImplementedException($"Unable to obtain the minimum value for the Open API schema. No matching NumericInfo type.")
                    };
                    schema.Maximum = numericInfo switch
                    {
                        NumericInfo<byte> byteInfo => byteInfo.Maximum,
                        NumericInfo<decimal> decimalInfo => decimalInfo.Maximum,
                        NumericInfo<double> doubleInfo => TryGetDecimal(doubleInfo.Maximum),
                        NumericInfo<int> intInfo => intInfo.Maximum,
                        NumericInfo<long> longInfo => longInfo.Maximum,
                        NumericInfo<sbyte> sbyteInfo => sbyteInfo.Maximum,
                        NumericInfo<short> shortInfo => shortInfo.Maximum,
                        NumericInfo<float> singleInfo => TryGetDecimal(singleInfo.Maximum),
                        NumericInfo<uint> uintInfo => uintInfo.Maximum,
                        NumericInfo<ulong> ulongInfo => ulongInfo.Maximum,
                        NumericInfo<ushort> ushortInfo => ushortInfo.Maximum,
                        _ => throw new NotImplementedException($"Unable to obtain the minimum value for the Open API schema. No matching NumericInfo type.")
                    };
                    schema.Format = numericInfo.DataType switch
                    {
                        DataType.Int => "int32",
                        DataType.Long => "int64",
                        DataType.Single => "float",
                        DataType.Double => "double",
                        _ => null
                    };

                    break;
                }

            default:
                break;
        }
    }

    private static decimal? TryGetDecimal(double minimum)
    {
        try
        {
            return Convert.ToDecimal(minimum);
        }
        catch (OverflowException)
        {
            return null;
        }
    }

    private static decimal? TryGetDecimal(float minimum)
    {
        try
        {
            return Convert.ToDecimal(minimum);
        }
        catch (OverflowException)
        {
            return null;
        }
    }
}
