
    public class ENCAPSULATED_PRIMITIVE_TYPEJsonConverter : System.Text.Json.Serialization.JsonConverter<ENCAPSULATED_PRIMITIVE_TYPE>
    {
        public override ENCAPSULATED_PRIMITIVE_TYPE Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
            => ENCAPSULATED_PRIMITIVE_TYPE.Parse(reader.GetString());

        public override void Write(System.Text.Json.Utf8JsonWriter writer, ENCAPSULATED_PRIMITIVE_TYPE value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
