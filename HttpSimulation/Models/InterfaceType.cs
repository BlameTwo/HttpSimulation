using HttpSimulation.Models.Enums;
using System.Text.Json.Serialization;

namespace HttpSimulation.Models;

public interface InterfaceType
{
    [JsonPropertyName("interfaceType")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("Type")]
    public RequestType Type { get;}
}