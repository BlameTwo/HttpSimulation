using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HttpSimulation.Models.InterfaceTypes.HttpInterfaces;

public partial class BodyFormData : ObservableObject
{
    [property: JsonPropertyName("name")]
    [ObservableProperty]
    string name;

    [property: JsonPropertyName("value")]
    [ObservableProperty]
    string value;

    [property: JsonPropertyName("by")]
    [ObservableProperty]
    string by;

    [property: JsonPropertyName("isFile")]
    [ObservableProperty]
    bool isFile;
}

public partial class BodyUrlEncodeData : ObservableObject
{
    [property: JsonPropertyName("name")]
    [ObservableProperty]
    string name;

    [property: JsonPropertyName("value")]
    [ObservableProperty]
    string value;

    [property: JsonPropertyName("by")]
    [ObservableProperty]
    string by;
}
