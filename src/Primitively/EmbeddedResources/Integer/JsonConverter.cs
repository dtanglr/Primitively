
    public class JsonConverter : System.Text.Json.Serialization.JsonConverter<PRIMITIVE_TYPE>
    {
        public override PRIMITIVE_TYPE Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
            => reader.PRIMITIVE_JSON_READER_METHOD(out var value) ? (PRIMITIVE_TYPE)value : default;

        public override void Write(System.Text.Json.Utf8JsonWriter writer, PRIMITIVE_TYPE value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value._value);
        }
    }
