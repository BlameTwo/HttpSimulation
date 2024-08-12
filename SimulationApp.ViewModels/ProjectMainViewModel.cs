using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models;
using HttpSimulation.Models.Enums;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using HttpSimulation.Services;
using SimulationApp.Contracts;
using WinUIExtentions;

namespace SimulationApp.ViewModels;

public sealed partial class ProjectMainViewModel : ObservableRecipient
{
    public ProjectMainViewModel(IDialogExtentionService dialogExtentionService)
    {
        DialogExtentionService = dialogExtentionService;
        this.IsActive = true;
    }

    [ObservableProperty]
    SimulationProjcet _ProjectData;

    public string Id { get; private set; }

    public void SetData(NavigationToProject project)
    {
        this.ProjectService.Load(project.project);
        this.Id = project.project.ID;
        this.ProjectData = project.project;
        this.SavePath = project.path;
        if (project.project.Interfaces == null)
        {
            this.Interfaces = new();
        }
        this.Interfaces = new(project.project.Interfaces);
    }

    [ObservableProperty]
    InterfaceType selectInterface;

    public IDialogExtentionService DialogExtentionService { get; }
    public ProjectService ProjectService => Setup.GetService<ProjectService>();
    public string SavePath { get; private set; }

    [ObservableProperty]
    ObservableCollection<InterfaceType> _Interfaces;

    [RelayCommand]
    async Task CreateInterfaceTask()
    {
        var folder = this
            .Interfaces.Where(x => x.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            .Select(x => x.Name);
        var result = await DialogExtentionService.CreateInterfaceAsync(new(folder));
        if (result == null)
            return;
        AddInterface(result);
    }

    public bool AddInterface(AddInterfaceResult result)
    {
        var folder = this
            .Interfaces.Where(x => x.Type == RequestType.Folder)
            .Select(x => x.Name)
            .ToList();
        if (folder.Contains(result.BaseFolder))
        {
            foreach (var item in Interfaces)
            {
                if (
                    item.Type == HttpSimulation.Models.Enums.RequestType.Folder
                    && item.Name == result.BaseFolder
                )
                {
                    (item as FolderInterface).Interfaces.Add(result.Interface);
                }
            }
            return false;
        }
        Interfaces.Add(result.Interface);
        return true;
    }

    [RelayCommand]
    void CrateInterfaceFolder()
    {
        AddFolder();
    }

    public bool AddFolder()
    {
        var name = ProjectService.GenerateNextFolderName(
            this.Interfaces.Select(x => x.Name).ToList()
        );
        this.Interfaces.Add(
            new FolderInterface()
            {
                ID = Guid.NewGuid().ToString("N").ToUpper(),
                Name = name,
                Interfaces = new()
            }
        );
        return true;
    }

    public async Task<bool> SaveAsync()
    {
        this.ProjectData.Interfaces = this.Interfaces;
        var result = (await this.ProjectData.SaveAsAsync(this.SavePath)) == null ? false : true;
        this.ProjectData = null;
        this.Interfaces.Clear();
        this.Interfaces = null;
        return result;
    }
}
