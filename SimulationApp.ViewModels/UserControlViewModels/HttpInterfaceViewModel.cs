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
    HttpInterface data;
    private string id;

    public IUserTabViewService UserTabViewService { get; }
    public ProjectService ProjectService { get; }

    public bool SaveData()
    {
        var value = ProjectService.UpdateHttpInterface(this.Data);
        UserTabViewService.UpdateHeader(id, $"{Data.Name}");
        return value;
    }

    public IRelayCommand SaveDataCommand => new RelayCommand(() => SaveData());

    public void SetData(HttpInterface param)
    {
        this.Data = param;
        this.id = Data.Name + Data.ID;
        this.ParamViewModel = this.CreateParamViewModel(Data.Data.GetParams ?? new());
        this.BodyViewModel = this.CreateBodyViewModel(Data.BodyData);
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
    HttpGetParamViewModel paramViewModel;

    [ObservableProperty]
    HttpBodyViewModel bodyViewModel;

    public HttpGetParamViewModel CreateParamViewModel(IEnumerable<HttpGetParam> httpGetParams)
    {
        var vm = Setup.GetService<HttpGetParamViewModel>();
        vm.GetParams = new(httpGetParams);
        return vm;
    }

    public HttpBodyViewModel CreateBodyViewModel(HttpBodyData httoBodyData)
    {
        var bodyVm = Setup.GetService<HttpBodyViewModel>();
        bodyVm.BodyData = httoBodyData;
        return bodyVm;
    }
}
