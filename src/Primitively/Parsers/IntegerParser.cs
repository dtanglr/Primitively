using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

internal class IntegerParser
{
    internal static bool TryParse(AttributeData attributeData, string name, string nameSpace, ParentData? parentData, DataType dataType, out RecordStructData? recordStructData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
        }

        if (string.IsNullOrEmpty(nameSpace))
        {
            throw new ArgumentException($"'{nameof(nameSpace)}' cannot be null or empty.", nameof(nameSpace));
        }

        recordStructData = InitRecordStructData(dataType, new RecordStructData(dataType, name, nameSpace, parentData));

        return TryParseNamedArguments(attributeData, recordStructData);
    }

    private static RecordStructData InitRecordStructData(DataType dataType, RecordStructData recordStructData)
    {
        switch (dataType)
        {
            case DataType.Byte:
                recordStructData.Interface = MetaData.Integer.Byte.Interface;
                recordStructData.Type = MetaData.Integer.Byte.Type;
                recordStructData.Example = MetaData.Integer.Byte.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Byte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.Byte.Minimum;
                recordStructData.Maximum = MetaData.Integer.Byte.Maximum;
                break;
            case DataType.SByte:
                recordStructData.Interface = MetaData.Integer.SByte.Interface;
                recordStructData.Type = MetaData.Integer.SByte.Type;
                recordStructData.Example = MetaData.Integer.SByte.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.SByte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.SByte.Minimum;
                recordStructData.Maximum = MetaData.Integer.SByte.Maximum;
                break;
            case DataType.Short:
                recordStructData.Interface = MetaData.Integer.Short.Interface;
                recordStructData.Type = MetaData.Integer.Short.Type;
                recordStructData.Example = MetaData.Integer.Short.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Short.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.Short.Minimum;
                recordStructData.Maximum = MetaData.Integer.Short.Maximum;
                break;
            case DataType.UShort:
                recordStructData.Interface = MetaData.Integer.UShort.Interface;
                recordStructData.Type = MetaData.Integer.UShort.Type;
                recordStructData.Example = MetaData.Integer.UShort.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.UShort.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.UShort.Minimum;
                recordStructData.Maximum = MetaData.Integer.UShort.Maximum;
                break;
            case DataType.Int:
                recordStructData.Interface = MetaData.Integer.Int.Interface;
                recordStructData.Type = MetaData.Integer.Int.Type;
                recordStructData.Example = MetaData.Integer.Int.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Int.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.Int.Minimum;
                recordStructData.Maximum = MetaData.Integer.Int.Maximum;
                break;
            case DataType.UInt:
                recordStructData.Interface = MetaData.Integer.UInt.Interface;
                recordStructData.Type = MetaData.Integer.UInt.Type;
                recordStructData.Example = MetaData.Integer.UInt.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.UInt.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.UInt.Minimum;
                recordStructData.Maximum = MetaData.Integer.UInt.Maximum;
                break;
            case DataType.Long:
                recordStructData.Interface = MetaData.Integer.Long.Interface;
                recordStructData.Type = MetaData.Integer.Long.Type;
                recordStructData.Example = MetaData.Integer.Long.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.Long.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.Long.Minimum;
                recordStructData.Maximum = MetaData.Integer.Long.Maximum;
                break;
            case DataType.ULong:
                recordStructData.Interface = MetaData.Integer.ULong.Interface;
                recordStructData.Type = MetaData.Integer.ULong.Type;
                recordStructData.Example = MetaData.Integer.ULong.Example;
                recordStructData.JsonReaderMethod = MetaData.Integer.ULong.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Integer.ULong.Minimum;
                recordStructData.Maximum = MetaData.Integer.ULong.Maximum;
                break;
            default:
                throw new NotSupportedException($"{dataType} is not supported");
        }

        return recordStructData;
    }

    private static bool TryParseNamedArguments(AttributeData attributeData, RecordStructData recordStructData)
    {
        if (attributeData.NamedArguments.IsEmpty)
        {
            return true;
        }

        var args = attributeData.NamedArguments;

        if (args.Any(a => a.Value.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        // Capture changes to Min and/or Max settings
        var rangeHasChanged = false;

        foreach (var arg in args)
        {
            var key = arg.Key;
            var value = arg.Value.Value;

            switch (key)
            {
                case "ImplementIValidatableObject":
                    recordStructData.ImplementIValidatableObject = (bool?)value ?? false;
                    break;
                case "Minimum":
                    recordStructData.Minimum = decimal.Parse(value?.ToString() ?? "0");
                    rangeHasChanged = true;
                    break;
                case "Maximum":
                    recordStructData.Maximum = decimal.Parse(value?.ToString() ?? "0");
                    rangeHasChanged = true;
                    break;
            }
        }

        // Set Example based on provided Min and Max settings
        if (rangeHasChanged)
        {
            var minimum = recordStructData.Minimum.GetValueOrDefault();
            var maximum = recordStructData.Maximum.GetValueOrDefault();
            var example = Math.Round(minimum + ((maximum - minimum) / 2));
            recordStructData.Example = example.ToString();
        }

        return true;
    }
}
