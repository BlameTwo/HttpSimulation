using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon.Core;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.InterfaceTypes.HttpInterfaces;
using HttpSimulation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.Contracts;
using SimulationApp.ViewModels.HttpViewModels;
using WinUIExtentions;

namespace SimulationApp.ViewModels.UserControlViewModels;

public partial class HttpInterfaceViewModel : ObservableObject
{
    public HttpInterfaceViewModel(IUserTabViewService userTabViewService)
    {
        UserTabViewService = userTabViewService;
    }

    [ObservableProperty]
    HttpInterface data;
    private string id;

    public IUserTabViewService UserTabViewService { get; }

    public ProjectService ProjectService =>
        Setup.ServiceProvider.GetRequiredService<ProjectService>();

    public bool SaveData()
    {
        this.Data.BodyData.GetParams = this.GetParams;
        this.Data.BodyData.FromData = this.FromData;
        this.Data.BodyData.CookieData = this.CookieData;
        this.Data.BodyData.FromUrlencode = this.FromUrlencode;
        this.Data.BodyData.XmlData = this.XmlStr;
        this.Data.BodyData.JsonData = this.JsonStr;
        this.Data.BodyData.RawData = this.RawStr;
        UserTabViewService.UpdateHeader(id, $"{Data.Name}");
        return ProjectService.UpdateHttpInterface(this.Data);
    }

    [ObservableProperty]
    bool xmlVisibility;

    [ObservableProperty]
    bool jsonVisibility;

    [ObservableProperty]
    bool rawVisibility;

    public IRelayCommand SaveDataCommand => new RelayCommand(() => SaveData());

    public void SetData(HttpInterface param)
    {
        this.Data = (HttpInterface)param.Clone();
        this.id = Data.Name + Data.ID;
        CreateBodyViewModel(Data.BodyData);
    }

    [ObservableProperty]
    string _bodyValue;

    [RelayCommand]
    void SetMethod(string method)
    {
        this.Data.Data.HttpMethod = method;
        UserTabViewService.UpdateHeader(id, $"{Data.Name}（已修改)");
    }

    [ObservableProperty]
    HttpBodyData bodyData;

    [ObservableProperty]
    PivotItem _selectItem;

    [ObservableProperty]
    string jsonStr;

    [ObservableProperty]
    string xmlStr;

    [ObservableProperty]
    ObservableCollection<HttpGetParam> getParams;

    [ObservableProperty]
    string rawStr;

    [ObservableProperty]
    ObservableCollection<HttpHeaderCookies> cookieData = new();

    [ObservableProperty]
    ObservableCollection<BodyUrlEncodeData> fromUrlencode = new();

    [ObservableProperty]
    ObservableCollection<BodyFormData> fromData = new();

    partial void OnSelectItemChanged(PivotItem value)
    {
        switch (value.Header.ToString())
        {
            case "json":
                this.JsonVisibility = true;
                this.XmlVisibility = false;
                this.RawVisibility = false;
                break;
            case "xml":
                this.JsonVisibility = false;
                this.XmlVisibility = true;
                this.RawVisibility = false;
                break;
            case "raw":
                this.JsonVisibility = false;
                this.XmlVisibility = false;
                this.RawVisibility = true;
                break;
        }
    }

    public HttpBodyViewModel CreateBodyViewModel(HttpBodyData httpbodyData)
    {
        var bodyVm = Setup.GetService<HttpBodyViewModel>();
        BodyData = httpbodyData;
        this.GetParams = httpbodyData.GetParams ?? new();
        this.FromData = httpbodyData.FromData ?? new();
        this.FromUrlencode = httpbodyData.FromUrlencode ?? new();
        this.CookieData = httpbodyData.CookieData ?? new();
        this.JsonStr = httpbodyData.JsonData;
        this.XmlStr = httpbodyData.XmlData;
        this.RawStr = httpbodyData.RawData;
        return bodyVm;
    }
}
