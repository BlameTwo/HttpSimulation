using CommunityToolkit.Mvvm.ComponentModel;

namespace HttpSimulation.Models.InterfaceTypes.HttpInterfaces;

public partial class HttpHeaderCookies : ObservableObject
{
    [ObservableProperty]
    public string key;

    [ObservableProperty]
    public string value;

    [ObservableProperty]
    public string by;
}
