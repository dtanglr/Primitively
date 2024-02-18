﻿
    public class JsonConverter : global::System.Text.Json.Serialization.JsonConverter<PRIMITIVE_TYPE>
    {
        public override PRIMITIVE_TYPE Read(ref global::System.Text.Json.Utf8JsonReader reader, global::System.Type typeToConvert, global::System.Text.Json.JsonSerializerOptions options)
            => PRIMITIVE_TYPE.Parse(reader.GetString());

        public override void Write(global::System.Text.Json.Utf8JsonWriter writer, PRIMITIVE_TYPE value, global::System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
