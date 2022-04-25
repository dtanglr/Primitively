using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Xunit;

namespace Primitively.IntegrationTests;

public abstract class JsonConverterTests<TJsonConverter, TPrimitive, TValueType>
    where TJsonConverter : JsonConverter<TPrimitive>, new()
    where TPrimitive : struct, IPrimitive<TValueType>
{
    protected abstract TPrimitive PrimitiveWithValue { get; }

    [Fact]
    public void JsonConverter_CanConvert()
    {
        var converter = new TJsonConverter();

        var canConvert = converter.CanConvert(typeof(TPrimitive));
        canConvert.Should().BeTrue();
    }

    [Fact]
    public void JsonConverter_CanReadValue()
    {
        var converter = new TJsonConverter();
        var json = $"\"{PrimitiveWithValue}\"";
        var bytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(bytes.AsSpan());
        reader.Read();

        var result = converter.Read(ref reader, typeof(TPrimitive), new JsonSerializerOptions());
        result.Should().BeAssignableTo(typeof(TPrimitive));
        result.Should().BeEquivalentTo(PrimitiveWithValue);
    }

    [Fact]
    public void JsonConverter_CanReadDefault()
    {
        var converter = new TJsonConverter();
        var json = $"\"{default(TPrimitive)}\"";
        var bytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(bytes.AsSpan());
        reader.Read();

        var result = converter.Read(ref reader, typeof(TPrimitive), new JsonSerializerOptions());
        result.Should().BeAssignableTo(typeof(TPrimitive));
        result.Should().BeEquivalentTo(default(TPrimitive));
    }

    [Fact]
    public void JsonConverter_CanReadNull()
    {
        var converter = new TJsonConverter();
        var json = "null";
        var bytes = Encoding.UTF8.GetBytes(json);
        var reader = new Utf8JsonReader(bytes.AsSpan());
        reader.Read();

        var result = converter.Read(ref reader, typeof(TPrimitive), new JsonSerializerOptions());
        result.Should().BeAssignableTo(typeof(TPrimitive));
        result.Should().BeEquivalentTo(default(TPrimitive));
    }

    [Fact]
    public void JsonConverter_CanWriteValue()
    {
        var bytes = new ArrayBufferWriter<byte>();
        var converter = new TJsonConverter();
        using var writer = new Utf8JsonWriter(bytes, new JsonWriterOptions { SkipValidation = true });

        converter.Write(writer, PrimitiveWithValue, new JsonSerializerOptions());
        writer.Flush();

        var json = Encoding.UTF8.GetString(bytes.WrittenSpan);
        json.Should().Be($"\"{PrimitiveWithValue}\"");
    }

    [Fact]
    public void JsonConverter_CanWriteDefault()
    {
        var primitive = default(TPrimitive);
        var bytes = new ArrayBufferWriter<byte>();
        var converter = new TJsonConverter();
        using var writer = new Utf8JsonWriter(bytes, new JsonWriterOptions { SkipValidation = true });

        converter.Write(writer, primitive, new JsonSerializerOptions());
        writer.Flush();

        var json = Encoding.UTF8.GetString(bytes.WrittenSpan);
        json.Should().Be(primitive.Value is null ? "null" : $"\"{primitive}\"");
    }
}
