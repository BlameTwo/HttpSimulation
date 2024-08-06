using HttpSimulation.Models;
using HttpSimulation.Models.Enums;
using HttpSimulation.Models.InterfaceTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HttpSimulation.Common;

public sealed class InterfaceJsonConverter : JsonConverter<InterfaceType> 
{
    public override InterfaceType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            if (!root.TryGetProperty("Type", out var type))
                return default;
            if (!(type is JsonElement element))
            {
                return default;
            }
            if (!element.TryGetInt32(out int value))
            {
                return default;
            }
            if(value == 1)
            {
                return JsonSerializer.Deserialize<HttpInterface>(root.GetRawText(),options);
            }
            // 根据某个标识符确定具体的类型
            return default;
        }
    }

    public override void Write(Utf8JsonWriter writer, InterfaceType value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
