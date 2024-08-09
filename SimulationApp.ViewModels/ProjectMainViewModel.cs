using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Contracts;
using HttpSimulation.Messagers;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Services;
using SimulationApp.Contracts;

namespace SimulationApp.ViewModels;

public sealed partial class ProjectMainViewModel
    : ObservableRecipient,
        IRecipient<ReInterfaceName>,
        IRecipient<RemoveInterface>
{
    public ProjectMainViewModel(
        IDialogExtentionService dialogExtentionService,
        IProjectService projectService
    )
    {
        DialogExtentionService = dialogExtentionService;
        ProjectService = projectService;
        this.IsActive = true;
    }

    [ObservableProperty]
    SimulationProjcet _ProjectData;

    public async Task SetDataAsync(string project)
    {
        await this.ProjectService.LoadAsync(project);
        //this.ProjectData = project;
        //this.Interfaces= new(project.Interfaces);
    }

    [ObservableProperty]
    InterfaceType selectInterface;

    public IDialogExtentionService DialogExtentionService { get; }
    public IProjectService ProjectService { get; }

    [RelayCommand]
    async Task CreateInterfaceTask()
    {
        var folder = this
            .ProjectService.Interfaces.Where(x =>
                x.Type == HttpSimulation.Models.Enums.RequestType.Folder
            )
            .Select(x => x.Name);
        var result = await DialogExtentionService.CreateInterfaceAsync(new(folder));
        if (result == null)
            return;
        ProjectService.AddInterface(result);
    }

    [RelayCommand]
    void CrateInterfaceFolder()
    {
        ProjectService.AddFolder();
    }

    [RelayCommand]
    void DFF() { }

    public async void Receive(ReInterfaceName message)
    {
        var result = await DialogExtentionService.CreateRenameResultAsync(message.Interface);
        if (result == null)
            return;
        ProjectService.ReName(ProjectService.Interfaces, result.id, result.newName);
    }

    public void Receive(RemoveInterface message)
    {
        ProjectService.Remove(ProjectService.Interfaces.ToList(), message.Interface);
    }
}
