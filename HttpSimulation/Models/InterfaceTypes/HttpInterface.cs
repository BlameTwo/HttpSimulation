using HttpSimulation.Models.Enums;
using System.Text.Json.Serialization;

namespace HttpSimulation.Models.InterfaceTypes;

public class HttpInterface : InterfaceType
{
    [JsonPropertyName("interfaceType")]
    public string Name { get; set; }

    [JsonPropertyName("id")]
    public string ID { get; set; }

    [JsonPropertyName("Type")]
    public RequestType Type => RequestType.Http;

    [JsonPropertyName("HttpMethod")]
    public string Method { get; set; }
}
