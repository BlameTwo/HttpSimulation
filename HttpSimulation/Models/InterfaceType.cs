using System.Text.Json.Serialization;
using HttpSimulation.Models.Enums;

namespace HttpSimulation.Models;

public interface InterfaceType : ICloneable
{
    [JsonPropertyName("interfaceType")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("Type")]
    public RequestType Type { get; }
}
