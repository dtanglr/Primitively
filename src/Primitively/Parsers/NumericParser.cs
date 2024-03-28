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

        return TryParseNamedArguments(dataType, attributeData, recordStructData);
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
                recordStructData.Interface = MetaData.Numeric.Byte.Interface;
                recordStructData.Type = MetaData.Numeric.Byte.Type;
                recordStructData.Example = MetaData.Numeric.Byte.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Byte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Byte.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Byte.Maximum;
                break;
            case DataType.SByte:
                recordStructData.Interface = MetaData.Numeric.SByte.Interface;
                recordStructData.Type = MetaData.Numeric.SByte.Type;
                recordStructData.Example = MetaData.Numeric.SByte.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.SByte.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.SByte.Minimum;
                recordStructData.Maximum = MetaData.Numeric.SByte.Maximum;
                break;
            case DataType.Short:
                recordStructData.Interface = MetaData.Numeric.Short.Interface;
                recordStructData.Type = MetaData.Numeric.Short.Type;
                recordStructData.Example = MetaData.Numeric.Short.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Short.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Short.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Short.Maximum;
                break;
            case DataType.UShort:
                recordStructData.Interface = MetaData.Numeric.UShort.Interface;
                recordStructData.Type = MetaData.Numeric.UShort.Type;
                recordStructData.Example = MetaData.Numeric.UShort.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.UShort.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.UShort.Minimum;
                recordStructData.Maximum = MetaData.Numeric.UShort.Maximum;
                break;
            case DataType.Int:
                recordStructData.Interface = MetaData.Numeric.Int.Interface;
                recordStructData.Type = MetaData.Numeric.Int.Type;
                recordStructData.Example = MetaData.Numeric.Int.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Int.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Int.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Int.Maximum;
                break;
            case DataType.UInt:
                recordStructData.Interface = MetaData.Numeric.UInt.Interface;
                recordStructData.Type = MetaData.Numeric.UInt.Type;
                recordStructData.Example = MetaData.Numeric.UInt.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.UInt.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.UInt.Minimum;
                recordStructData.Maximum = MetaData.Numeric.UInt.Maximum;
                break;
            case DataType.Long:
                recordStructData.Interface = MetaData.Numeric.Long.Interface;
                recordStructData.Type = MetaData.Numeric.Long.Type;
                recordStructData.Example = MetaData.Numeric.Long.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Long.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Long.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Long.Maximum;
                break;
            case DataType.ULong:
                recordStructData.Interface = MetaData.Numeric.ULong.Interface;
                recordStructData.Type = MetaData.Numeric.ULong.Type;
                recordStructData.Example = MetaData.Numeric.ULong.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.ULong.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.ULong.Minimum;
                recordStructData.Maximum = MetaData.Numeric.ULong.Maximum;
                break;
            case DataType.Single:
                recordStructData.Interface = MetaData.Numeric.Single.Interface;
                recordStructData.Type = MetaData.Numeric.Single.Type;
                recordStructData.Example = MetaData.Numeric.Single.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Single.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Single.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Single.Maximum;
                break;
            case DataType.Double:
                recordStructData.Interface = MetaData.Numeric.Double.Interface;
                recordStructData.Type = MetaData.Numeric.Double.Type;
                recordStructData.Example = MetaData.Numeric.Double.Example;
                recordStructData.JsonReaderMethod = MetaData.Numeric.Double.JsonReaderMethod;
                recordStructData.Minimum = MetaData.Numeric.Double.Minimum;
                recordStructData.Maximum = MetaData.Numeric.Double.Maximum;
                recordStructData.Digits = MetaData.Numeric.Double.Digits;
                recordStructData.Mode = MetaData.Numeric.Double.Mode;
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
        // Only parse constructor arguments for Double Attribute data type
        if (dataType != DataType.Double)
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

        var digits = (int)args[0].Value!;
        digits = digits < MetaData.Numeric.Double.Digits ? MetaData.Numeric.Double.Digits : digits;
        recordStructData.Digits = digits;

        var mode = MetaData.Numeric.Double.Mode;

        if (args.Length == 2)
        {
            mode = (MidpointRounding)args[1].Value!;
            recordStructData.Mode = mode;
        }

        if (digits > MetaData.Numeric.Double.Digits)
        {
            var minimum = Math.Round(MetaData.Numeric.Double.Minimum, digits, mode);
            var maximum = Math.Round(MetaData.Numeric.Double.Maximum, digits, mode);
            var example = Math.Round(maximum / 2, digits, mode);
            recordStructData.Minimum = minimum;
            recordStructData.Maximum = maximum;
            recordStructData.Example = example.ToString();
        }

        return true;
    }

    /// <summary>
    /// Attempts to parse the named arguments of the specified attribute data into a record struct data.
    /// </summary>
    /// <param name="dataType">The data type of the record struct.</param>
    /// <param name="attributeData">The attribute data whose named arguments to parse.</param>
    /// <param name="recordStructData">The record struct data to populate with the parsed named arguments.</param>
    /// <returns>true if the named arguments were parsed successfully; otherwise, false.</returns>
    private static bool TryParseNamedArguments(DataType dataType, AttributeData attributeData, RecordStructData recordStructData)
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
                case nameof(NumericAttribute.Minimum) when dataType == DataType.Double:
                    {
                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > MetaData.Numeric.Double.Digits)
                        {
                            var digits = recordStructData.Digits.Value;
                            var mode = recordStructData.Mode ?? MetaData.Numeric.Double.Mode;
                            recordStructData.Minimum = Math.Round(Convert.ToDouble(value), digits, mode);
                            rangeHasChanged = true;
                            break;
                        }

                        recordStructData.Minimum = value;
                        rangeHasChanged = true;
                        break;
                    }
                case nameof(NumericAttribute.Minimum):
                    recordStructData.Minimum = value;
                    rangeHasChanged = true;
                    break;
                case nameof(NumericAttribute.Maximum) when dataType == DataType.Double:
                    {
                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > MetaData.Numeric.Double.Digits)
                        {
                            var digits = recordStructData.Digits.Value;
                            var mode = recordStructData.Mode ?? MetaData.Numeric.Double.Mode;
                            recordStructData.Maximum = Math.Round(Convert.ToDouble(value), digits, mode);
                            rangeHasChanged = true;
                            break;
                        }

                        recordStructData.Maximum = value;
                        rangeHasChanged = true;
                        break;
                    }
                case nameof(NumericAttribute.Maximum):
                    recordStructData.Maximum = value;
                    rangeHasChanged = true;
                    break;
                default:
                    break;
            }
        }

        // Set Example based on provided Min and Max settings
        if (rangeHasChanged)
        {
            var example = recordStructData.Example;

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
                        var minimum = Convert.ToDecimal(recordStructData.Minimum);
                        var maximum = Convert.ToDecimal(recordStructData.Maximum);
                        recordStructData.Example = Math.Round(minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                case DataType.Single:
                    {
                        var minimum = Convert.ToSingle(recordStructData.Minimum);
                        var maximum = Convert.ToSingle(recordStructData.Maximum);
                        recordStructData.Example = Math.Round(minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                case DataType.Double:
                    {
                        var minimum = Convert.ToDouble(recordStructData.Minimum);
                        var maximum = Convert.ToDouble(recordStructData.Maximum);

                        if (recordStructData.Digits.HasValue && recordStructData.Digits.Value > MetaData.Numeric.Double.Digits)
                        {
                            var digits = recordStructData.Digits.Value;
                            var mode = recordStructData.Mode ?? MetaData.Numeric.Double.Mode;
                            recordStructData.Example = Math.Round(minimum + ((maximum - minimum) / 2), digits, mode).ToString();
                            break;
                        }

                        recordStructData.Example = Math.Round(minimum + ((maximum - minimum) / 2)).ToString();
                        break;
                    }
                default:
                    break;
            }
        }

        return true;
    }
}
