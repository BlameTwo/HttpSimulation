using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimulationApp.Contracts;
using System.Threading.Tasks;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels;

public sealed partial class ShellViewModel:ObservableObject
{
    public ShellViewModel(IApplicationSetup<ClientApplication> applicationSetup,IDialogExtentionService dialogExtentionService)
    {
        ApplicationSetup=applicationSetup;
        DialogExtentionService=dialogExtentionService;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }
    public IDialogExtentionService DialogExtentionService { get; }

    [RelayCommand]
    async Task ShowCreateProject()
    {
        await DialogExtentionService.ShowCreateProjectAsync();
    }
}
