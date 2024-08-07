using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Primitively.Parsers;

/// <summary>
/// Provides methods for parsing Integer attribute data.
/// </summary>
internal static class NumericParser
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

        if (!TryParseAttributeConstructorArguments(dataType, attributeData, recordStructData))
        {
            return false;
        }

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
                recordStructData.Interface = MetaData.Numeric.Integer.Byte.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.Byte.Type;
                recordStructData.Example = MetaData.Numeric.Integer.Byte.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.Byte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.Byte.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.Byte.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.Byte.InfoType;
                break;
            case DataType.Decimal:
                recordStructData.Interface = MetaData.Numeric.FloatingPoint.Decimal.Interface;
                recordStructData.Type = MetaData.Numeric.FloatingPoint.Decimal.Type;
                recordStructData.Example = MetaData.Numeric.FloatingPoint.Decimal.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.FloatingPoint.Decimal.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.FloatingPoint.Decimal.Minimum;
                recordStructData.Maximum = MetaData.Numeric.FloatingPoint.Decimal.Maximum;
                recordStructData.Digits = MetaData.Numeric.FloatingPoint.Digits;
                recordStructData.Mode = MetaData.Numeric.FloatingPoint.Mode;
                recordStructData.InfoType = MetaData.Numeric.FloatingPoint.Decimal.InfoType;
                break;
            case DataType.Double:
                recordStructData.Interface = MetaData.Numeric.FloatingPoint.Double.Interface;
                recordStructData.Type = MetaData.Numeric.FloatingPoint.Double.Type;
                recordStructData.Example = MetaData.Numeric.FloatingPoint.Double.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.FloatingPoint.Double.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.FloatingPoint.Double.Minimum;
                recordStructData.Maximum = MetaData.Numeric.FloatingPoint.Double.Maximum;
                recordStructData.Digits = MetaData.Numeric.FloatingPoint.Digits;
                recordStructData.Mode = MetaData.Numeric.FloatingPoint.Mode;
                recordStructData.InfoType = MetaData.Numeric.FloatingPoint.Double.InfoType;
                break;
            case DataType.Int:
                recordStructData.Interface = MetaData.Numeric.Integer.Int.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.Int.Type;
                recordStructData.Example = MetaData.Numeric.Integer.Int.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.Int.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.Int.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.Int.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.Int.InfoType;
                break;
            case DataType.Long:
                recordStructData.Interface = MetaData.Numeric.Integer.Long.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.Long.Type;
                recordStructData.Example = MetaData.Numeric.Integer.Long.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.Long.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.Long.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.Long.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.Long.InfoType;
                break;
            case DataType.SByte:
                recordStructData.Interface = MetaData.Numeric.Integer.SByte.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.SByte.Type;
                recordStructData.Example = MetaData.Numeric.Integer.SByte.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.SByte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.SByte.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.SByte.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.SByte.InfoType;
                break;
            case DataType.Short:
                recordStructData.Interface = MetaData.Numeric.Integer.Short.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.Short.Type;
                recordStructData.Example = MetaData.Numeric.Integer.Short.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.Short.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.Short.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.Short.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.Short.InfoType;
                break;
            case DataType.Single:
                recordStructData.Interface = MetaData.Numeric.FloatingPoint.Single.Interface;
                recordStructData.Type = MetaData.Numeric.FloatingPoint.Single.Type;
                recordStructData.Example = MetaData.Numeric.FloatingPoint.Single.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.FloatingPoint.Single.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.FloatingPoint.Single.Minimum;
                recordStructData.Maximum = MetaData.Numeric.FloatingPoint.Single.Maximum;
                recordStructData.Digits = MetaData.Numeric.FloatingPoint.Digits;
                recordStructData.Mode = MetaData.Numeric.FloatingPoint.Mode;
                recordStructData.InfoType = MetaData.Numeric.FloatingPoint.Single.InfoType;
                break;
            case DataType.UInt:
                recordStructData.Interface = MetaData.Numeric.Integer.UInt.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.UInt.Type;
                recordStructData.Example = MetaData.Numeric.Integer.UInt.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.UInt.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.UInt.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.UInt.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.UInt.InfoType;
                break;
            case DataType.ULong:
                recordStructData.Interface = MetaData.Numeric.Integer.ULong.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.ULong.Type;
                recordStructData.Example = MetaData.Numeric.Integer.ULong.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.ULong.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.ULong.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.ULong.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.ULong.InfoType;
                break;
            case DataType.UShort:
                recordStructData.Interface = MetaData.Numeric.Integer.UShort.Interface;
                recordStructData.Type = MetaData.Numeric.Integer.UShort.Type;
                recordStructData.Example = MetaData.Numeric.Integer.UShort.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Integer.UShort.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Integer.UShort.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Integer.UShort.Maximum;
                recordStructData.InfoType = MetaData.Numeric.Integer.UShort.InfoType;
                break;
            default:
                throw new NotSupportedException($"{dataType} is not supported");
        }

        return recordStructData;
    }

    /// <summary>
    /// Attempts to parse the constructor arguments of the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="dataType">The data type of the record struct.</param>
    /// <param name="attributeData">The attribute data whose constructor arguments to parse.</param>
    /// <param name="recordStructData">The record struct data to populate with the parsed constructor arguments.</param>
    /// <returns>true if the constructor arguments were parsed successfully; otherwise, false.</returns>
    private static bool TryParseAttributeConstructorArguments(DataType dataType, AttributeData attributeData, RecordStructData recordStructData)
    {
        // Only parse constructor arguments for Double & Single Attribute data types
        if (dataType != DataType.Decimal && dataType != DataType.Double && dataType != DataType.Single)
        {
            return true;
        }

        if (attributeData.ConstructorArguments.IsEmpty)
        {
            return true;
        }

        var args = attributeData.ConstructorArguments;

        if (args.Length > 2 || args.Any(a => a.Kind == TypedConstantKind.Error))
        {
            return false;
        }

        const int DefaultDigits = MetaData.Numeric.FloatingPoint.Digits;
        const int MinDigits = MetaData.Numeric.FloatingPoint.MinDigits;
        var maxDigits = dataType switch
        {
            DataType.Decimal => MetaData.Numeric.FloatingPoint.Decimal.MaxDigits,
            DataType.Double => MetaData.Numeric.FloatingPoint.Double.MaxDigits,
            DataType.Single => MetaData.Numeric.FloatingPoint.Single.MaxDigits,
            _ => throw new NotSupportedException($"{dataType} is not supported")
        };

        var digits = (int)args[0].Value!;
        digits = (digits < MinDigits) || (digits > maxDigits) ? DefaultDigits : digits;
        recordStructData.Digits = digits;

        if (args.Length == 2)
        {
            var mode = (MidpointRounding)args[1].Value!;
            recordStructData.Mode = mode;
        }

        if (digits > DefaultDigits)
        {
            recordStructData.Example = 123.123456789012345.ToString($"N{digits}");
        }

        return true;
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

            if (value is null)
            {
                continue;
            }

            switch (key)
            {
                case nameof(NumericAttribute.ImplementIValidatableObject):
                    recordStructData.ImplementIValidatableObject = Convert.ToBoolean(value!);
                    break;
                case nameof(NumericAttribute.Minimum):
                    recordStructData.Minimum = value;
                    rangeHasChanged = true;
                    break;
                case nameof(NumericAttribute.Maximum):
                    recordStructData.Maximum = value;
                    rangeHasChanged = true;
                    break;
                default:
                    break;
            }
        }

        // Set Example based on provided Min and Max settings
        // TODO: Fix potential exeeptions in the calculation for the example value
        if (rangeHasChanged)
        {
            switch (recordStructData.DataType)
            {
                case DataType.Byte:
                case DataType.SByte:
                case DataType.Short:
                case DataType.UShort:
                case DataType.Int:
                case DataType.UInt:
                case DataType.Long:
                case DataType.ULong:
                    {
                        var range = new decimal[]
                        {
                            Convert.ToDecimal(recordStructData.Minimum),
                            Convert.ToDecimal(recordStructData.Maximum)
                        }
                        .OrderBy(r => r)
                        .ToArray();

                        var minimum = range[0];
                        var maximum = range[1];
                        recordStructData.Minimum = minimum;
                        recordStructData.Maximum = maximum;
                        recordStructData.Example = Math.Round(minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                case DataType.Decimal:
                    {
                        var minimum = Convert.ToDecimal(recordStructData.Minimum);
                        var maximum = Convert.ToDecimal(recordStructData.Maximum);
                        var defaultDigits = MetaData.Numeric.FloatingPoint.Digits;

                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > defaultDigits)
                        {
                            var digits = recordStructData.Digits.Value;
                            recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString($"N{digits}");
                            break;
                        }

                        recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                case DataType.Double:
                    {
                        var minimum = Convert.ToDouble(recordStructData.Minimum);
                        var maximum = Convert.ToDouble(recordStructData.Maximum);
                        var defaultDigits = MetaData.Numeric.FloatingPoint.Digits;

                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > defaultDigits)
                        {
                            var digits = recordStructData.Digits.Value;
                            recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString($"N{digits}");
                            break;
                        }

                        recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                case DataType.Single:
                    {
                        var minimum = Convert.ToSingle(recordStructData.Minimum);
                        var maximum = Convert.ToSingle(recordStructData.Maximum);
                        var defaultDigits = MetaData.Numeric.FloatingPoint.Digits;

                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > defaultDigits)
                        {
                            var digits = recordStructData.Digits.Value;
                            recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString($"N{digits}");
                            break;
                        }

                        recordStructData.Example = (minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                default:
                    break;
            }
        }

        return true;
    }
}
