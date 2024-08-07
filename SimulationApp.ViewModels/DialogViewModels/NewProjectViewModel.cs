using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Contracts;
using HttpSimulation.Models;
using System.Threading.Tasks;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels;

public sealed partial class NewProjectViewModel:ObservableObject
{
    public IProjectService ProjectService { get; }
    public IDialogManager DialogManager { get; }

    public NewProjectViewModel(IProjectService projectService,IDialogManager dialogManager)
    {
        ProjectService=projectService;
        DialogManager=dialogManager;
    }

    [ObservableProperty]
    string projectName;

    [RelayCommand]
    void Cancel()
    {
        DialogManager.CloseDialog();
    }

    [RelayCommand]
    async Task CreateAsync()
    {
        var p = await SimulationProjcet.ParseAsync("D:\\Test.zip");
        var project =  ProjectService.CreateProject(ProjectName);
        project = await project.SaveAsAsync("D:\\Test.zip");
    }

}