using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

/// <summary>
/// Provides methods for parsing Integer attribute data.
/// </summary>
internal static class IntegerParser
{
    /// <summary>
    /// Attempts to parse the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="attributeData">The attribute data to parse.</param>
    /// <param name="name">The name of the record struct.</param>
    /// <param name="nameSpace">The namespace of the record struct.</param>
    /// <param name="parentData">The parent data of the record struct.</param>
    /// <param name="dataType">The data type of the record struct.</param>
    /// <param name="recordStructData">When this method returns, contains the parsed record struct data, if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the attribute data was parsed successfully; otherwise, false.</returns>
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

    /// <summary>
    /// Initializes a record struct data with the specified data type.
    /// </summary>
    /// <param name="dataType">The data type of the record struct.</param>
    /// <param name="recordStructData">The record struct data to initialize.</param>
    /// <returns>The initialized record struct data.</returns>
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

    /// <summary>
    /// Attempts to parse the named arguments of the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="attributeData">The attribute data whose named arguments to parse.</param>
    /// <param name="recordStructData">The record struct data to populate with the parsed named arguments.</param>
    /// <returns>true if the named arguments were parsed successfully; otherwise, false.</returns>
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
                case nameof(IntegerAttribute.ImplementIValidatableObject):
                    recordStructData.ImplementIValidatableObject = (bool?)value ?? false;
                    break;
                case nameof(IntegerAttribute.Minimum):
                    recordStructData.Minimum = decimal.TryParse(value?.ToString(), out var minimum) ? minimum : recordStructData.Minimum;
                    rangeHasChanged = true;
                    break;
                case nameof(IntegerAttribute.Maximum):
                    recordStructData.Maximum = decimal.TryParse(value?.ToString(), out var maximum) ? maximum : recordStructData.Maximum;
                    rangeHasChanged = true;
                    break;
                default:
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
