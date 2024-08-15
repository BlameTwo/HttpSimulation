using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models.InterfaceTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimulationApp.ViewModels.HttpViewModels;

public partial class HttpBodyViewModel : ObservableObject
{
    [ObservableProperty]
    HttpBodyData bodyData;

    [ObservableProperty]
    PivotItem _selectItem;

    public HttpBodyViewModel() { }

    partial void OnSelectItemChanged(PivotItem value)
    {
        this.BodyData.SelectBody = value.Header.ToString();
        switch (value.Header.ToString())
        {
            case "raw":
                RawVisibility = true;
                JsonVisibility = false;
                XmlVisibility = false;
                break;
            case "xml":
                RawVisibility = false;
                JsonVisibility = false;
                XmlVisibility = true;
                break;
            case "json":
                RawVisibility = false;
                JsonVisibility = true;
                XmlVisibility = false;
                break;
            default:
                RawVisibility = false;
                JsonVisibility = false;
                XmlVisibility = false;
                break;
        }
    }

    [ObservableProperty]
    string jsonStr;

    [ObservableProperty]
    string xmlStr;

    [ObservableProperty]
    string rawStr;

    [ObservableProperty]
    bool xmlVisibility;

    [ObservableProperty]
    bool jsonVisibility;

    [ObservableProperty]
    bool rawVisibility;
}
