using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Models;
using HttpSimulation.Models.Operation;
using HttpSimulation.Services;
using SimulationApp.Contracts;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class AddProjectViewModel
    : ObservableObject,
        IContentDialogViewModel<CreateProjectParam, CreateProjectResult>
{
    public ProjectService ProjectService { get; }
    public IPickersService PickersService { get; }
    public IDialogManager DialogManager { get; }

    [ObservableProperty]
    public CreateProjectParam data;

    public AddProjectViewModel(
        ProjectService projectService,
        IPickersService pickersService,
        IDialogManager dialogManager
    )
    {
        ProjectService = projectService;
        PickersService = pickersService;
        DialogManager = dialogManager;
    }

    [ObservableProperty]
    string projectName;

    [ObservableProperty]
    string savePath;

    public async Task GetSavePath()
    {
        var picker = PickersService.GetFileSavePicker([".zip"]);
        var file = await picker.PickSaveFileAsync();
        if (file == null)
            return;
        await file.DeleteAsync();
        this.SavePath = file.Path;
    }

    public CreateProjectResult Build()
    {
        var project = ProjectService.CreateProject(ProjectName);
        project.Interfaces = new List<InterfaceType>();
        return new(this.SavePath, project);
    }

    public void Update()
    {
        this.ProjectName = Data.name;
    }
}
