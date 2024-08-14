using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.InterfaceTypes.HttpInterfaces;
using HttpSimulation.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.Contracts;
using SimulationApp.ViewModels.HttpViewModels;
using WinUIExtentions;

namespace SimulationApp.ViewModels.UserControlViewModels;

public partial class HttpInterfaceViewModel : ObservableObject
{
    public HttpInterfaceViewModel(
        IUserTabViewService userTabViewService,
        ProjectService projectService
    )
    {
        UserTabViewService = userTabViewService;
        ProjectService = projectService;
    }

    [ObservableProperty]
    HttpInterface method;
    private string id;

    public IUserTabViewService UserTabViewService { get; }
    public ProjectService ProjectService { get; }

    public bool SaveData()
    {
        var value = ProjectService.UpdateHttpInterface(this.Method);
        UserTabViewService.UpdateHeader(id, $"{Method.Name}");
        return value;
    }

    public IRelayCommand SaveDataCommand => new RelayCommand(() => SaveData());

    public void SetData(HttpInterface param)
    {
        this.Method = param;
        this.id = Method.Name + Method.ID;
        this.ParamViewModel = this.CreateParamViewModel(Method.Data.GetParams ?? new());
    }

    [RelayCommand]
    void SetMethod(string method)
    {
        this.Method.Data.HttpMethod = method;
        UserTabViewService.UpdateHeader(id, $"{Method.Name}（已修改)");
    }

    [ObservableProperty]
    HttpGetParamViewModel paramViewModel;

    [ObservableProperty]
    SelectorBarItem _selectorBaritem;

    #region Params Visibility
    [ObservableProperty]
    Visibility setParamVisibility = Visibility.Collapsed;

    [ObservableProperty]
    Visibility bodyVisibility = Visibility.Collapsed;

    [ObservableProperty]
    Visibility cookieVisibility = Visibility.Collapsed;

    [ObservableProperty]
    Visibility authVisibility = Visibility.Collapsed;
    #endregion

    partial void OnSelectorBaritemChanged(SelectorBarItem value)
    {
        switch (value.Text)
        {
            case "Params":
                SetParamVisibility = Visibility.Visible;
                BodyVisibility = Visibility.Collapsed;
                CookieVisibility = Visibility.Collapsed;
                AuthVisibility = Visibility.Collapsed;
                break;
            case "Body":
                SetParamVisibility = Visibility.Collapsed;
                BodyVisibility = Visibility.Visible;
                CookieVisibility = Visibility.Collapsed;
                AuthVisibility = Visibility.Collapsed;
                break;
            case "Cookie":
                SetParamVisibility = Visibility.Collapsed;
                BodyVisibility = Visibility.Collapsed;
                CookieVisibility = Visibility.Visible;
                AuthVisibility = Visibility.Collapsed;
                break;
            case "Auth":
                SetParamVisibility = Visibility.Collapsed;
                BodyVisibility = Visibility.Collapsed;
                CookieVisibility = Visibility.Collapsed;
                AuthVisibility = Visibility.Visible;
                break;
        }
    }

    [RelayCommand]
    async Task SendAsync() { }

    public HttpGetParamViewModel CreateParamViewModel(IEnumerable<HttpGetParam> httpGetParams)
    {
        var vm = Setup.GetService<HttpGetParamViewModel>();
        vm.GetParams = new(httpGetParams);
        return vm;
    }
}
