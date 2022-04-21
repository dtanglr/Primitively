
    public class PRIMITIVE_TYPEJsonConverter : System.Text.Json.Serialization.JsonConverter<PRIMITIVE_TYPE>
    {
        public override PRIMITIVE_TYPE Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
            => PRIMITIVE_TYPE.Parse(reader.GetString());

        public override void Write(System.Text.Json.Utf8JsonWriter writer, PRIMITIVE_TYPE value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
