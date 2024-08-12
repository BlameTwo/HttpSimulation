using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Shapes;
using SimulationApp.Contracts;
using WinUIExtentions;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;
using WinUIExtentions.Contracts.TabView;

namespace SimulationApp.ViewModels;

public sealed partial class ShellViewModel
    : ObservableRecipient,
        IRecipient<ReInterfaceName>,
        IRecipient<RemoveInterface>,
        IRecipient<OpenInterface>
{
    public ShellViewModel(
        IApplicationSetup<ClientApplication> applicationSetup,
        IDialogExtentionService dialogExtentionService,
        IPickersService pickersService,
        IUserTabViewService userTabViewService,
        ITabViewService tabViewService,
        ProjectService projectService
    )
    {
        ApplicationSetup = applicationSetup;
        DialogExtentionService = dialogExtentionService;
        PickersService = pickersService;
        UserTabViewService = userTabViewService;
        TabViewService = tabViewService;
        ProjectService = projectService;
        this.IsActive = true;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }
    public IDialogExtentionService DialogExtentionService { get; }
    public IPickersService PickersService { get; }
    public IUserTabViewService UserTabViewService { get; }
    public ITabViewService TabViewService { get; }
    public ProjectService ProjectService { get; }

    [RelayCommand]
    async Task ShowCreateProject()
    {
        var dialogResult = await DialogExtentionService.CreateProjectAsync("新项目");
        if (dialogResult == null)
            return;
        await dialogResult.project.SaveAsAsync(dialogResult.path);
        await ProjectService.LoadAsync(dialogResult.path);
    }

    [RelayCommand]
    void Loaded()
    {
        UserTabViewService.OpenHome();
    }

    [RelayCommand]
    async Task OpenProjectAsync()
    {
        var picker = PickersService.GetFileOpenPicker(new string[] { ".zip" });
        var result = await picker.PickSingleFileAsync();
        if (result == null)
            return;
        var proj = await ProjectService.LoadAsync(result.Path);
    }

    [RelayCommand]
    async Task SaveProjectAsync()
    {
        var result = await ProjectService.SaveAsync();
    }

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
        AddInterface(result);
    }

    public bool AddInterface(AddInterfaceResult result)
    {
        var folder = this
            .ProjectService.Interfaces.Where(x => x.Type == RequestType.Folder)
            .Select(x => x.Name)
            .ToList();
        if (folder.Contains(result.BaseFolder))
        {
            foreach (var item in ProjectService.Interfaces)
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
        ProjectService.Interfaces.Add(result.Interface);
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
            this.ProjectService.Interfaces.Select(x => x.Name).ToList()
        );
        this.ProjectService.Interfaces.Add(
            new FolderInterface()
            {
                ID = Guid.NewGuid().ToString("N").ToUpper(),
                Name = name,
                Interfaces = new()
            }
        );
        return true;
    }

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
        this.UserTabViewService.CloseTab(message.Interface.Name + message.Interface.ID);
    }

    public void Receive(OpenInterface message)
    {
        UserTabViewService.OpenInterface(message.Interface);
    }
}
