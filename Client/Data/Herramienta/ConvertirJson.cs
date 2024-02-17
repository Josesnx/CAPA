using System.Text.Json.Serialization;
using System.Text.Json;

namespace Client.Data.Herramienta;

public class ConvertirJson<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string jsonStr = reader.GetString();
        return JsonSerializer.Deserialize<T>(jsonStr, options);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        string jsonStr = JsonSerializer.Serialize(value, options);
        writer.WriteStringValue(jsonStr);
    }
}
