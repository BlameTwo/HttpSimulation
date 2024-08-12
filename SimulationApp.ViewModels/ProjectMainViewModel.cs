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
using HttpSimulation.Contracts;
using HttpSimulation.Messagers;
using HttpSimulation.Models;
using HttpSimulation.Models.Enums;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
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
    public IProjectService ProjectService { get; }
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
        var name = GenerateNextFolderName(this.Interfaces.Select(x => x.Name).ToList());
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

    public void ReName(ObservableCollection<InterfaceType> interfaces, string id, string newName)
    {
        foreach (var iface in interfaces)
        {
            if (iface.ID == id)
            {
                iface.Name = newName;
            }
            if (iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                var list = iface as FolderInterface;
                ReName(list.Interfaces, id, newName);
            }
        }
    }

    public void Remove(IEnumerable<InterfaceType> types, InterfaceType message)
    {
        foreach (var iface in types)
        {
            if (iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                if (iface.ID == message.ID)
                {
                    Interfaces.Remove(iface);
                }
                var list = iface as FolderInterface;
                var remove = RemoveFolder(list.Interfaces, message);
                if (remove == null)
                    continue;
                list.Interfaces = new(remove);
                break;
            }
            if (iface.ID == message.ID)
            {
                Interfaces.Remove(iface);
            }
        }
    }

    private IEnumerable<InterfaceType>? RemoveFolder(
        ObservableCollection<InterfaceType> interfaces,
        InterfaceType message
    )
    {
        foreach (var iface in interfaces)
        {
            var list = iface as FolderInterface;
            foreach (var iface2 in interfaces.ToList())
            {
                if (iface2.ID == message.ID)
                {
                    interfaces.Remove(iface2);
                    return interfaces;
                }
            }
        }
        return null;
    }

    public string GenerateNextFolderName(List<string> folders)
    {
        int maxNumber = 0;
        bool hasMatchingFolders = false;

        foreach (var folder in folders)
        {
            var match = Regex.Match(folder, @"新建文件夹\((\d+)\)");
            if (match.Success)
            {
                hasMatchingFolders = true;
                int number = int.Parse(match.Groups[1].Value);
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }
        }
        if (!hasMatchingFolders)
        {
            maxNumber = 0;
        }

        // 生成新的文件夹名
        return $"新建文件夹({maxNumber + 1})";
    }

    public async void Receive(ReInterfaceName message)
    {
        var result = await DialogExtentionService.CreateRenameResultAsync(message.Interface);
        if (result == null)
            return;
        ReName(Interfaces, result.id, result.newName);
    }

    public void Receive(RemoveInterface message)
    {
        Remove(Interfaces.ToList(), message.Interface);
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
