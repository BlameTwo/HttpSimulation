using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Services;
using SimulationApp.Contracts;

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
    }

    [RelayCommand]
    void SetMethod(string method)
    {
        this.Method.Data.HttpMethod = method;
        UserTabViewService.UpdateHeader(id, $"{Method.Name}（已修改)");
    }
}
