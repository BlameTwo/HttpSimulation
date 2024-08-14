using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HttpSimulation.Models.InterfaceTypes.HttpInterfaces;

public partial class HttpGetParam : ObservableObject
{
    [property: JsonPropertyName("name")]
    [ObservableProperty]
    [property: MaybeNull]
    string name;

    [property: JsonPropertyName("value")]
    [ObservableProperty]
    [property: MaybeNull]
    string value;

    [property: JsonPropertyName("by")]
    [ObservableProperty]
    [property: NotNull]
    string? by;
}
