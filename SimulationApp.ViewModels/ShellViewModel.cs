using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Contracts;
using HttpSimulation.Models;
using Microsoft.Extensions.DependencyInjection;
using SimulationApp.Contracts;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;
using WinUIExtentions.Contracts.TabView;

namespace SimulationApp.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    public ShellViewModel(
        IApplicationSetup<ClientApplication> applicationSetup,
        IDialogExtentionService dialogExtentionService,
        IPickersService pickersService,
        IProjectService projectService,
        IUserTabViewService userTabViewService,
        ITabViewService tabViewService
    )
    {
        ApplicationSetup = applicationSetup;
        DialogExtentionService = dialogExtentionService;
        PickersService = pickersService;
        ProjectService = projectService;
        UserTabViewService = userTabViewService;
        TabViewService = tabViewService;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }
    public IDialogExtentionService DialogExtentionService { get; }
    public IPickersService PickersService { get; }
    public IProjectService ProjectService { get; }
    public IUserTabViewService UserTabViewService { get; }
    public ITabViewService TabViewService { get; }

    [RelayCommand]
    async Task ShowCreateProject()
    {
        var dialogResult = await DialogExtentionService.CreateProjectAsync("新项目");
        await dialogResult.project.SaveAsAsync(dialogResult.path);
        await UserTabViewService.OpenProjectAsync(dialogResult.path);
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
        if (result != null)
        {
            //var projcet = await SimulationProjcet.ParseAsync((await result.OpenReadAsync()).AsStreamForRead());
            //MainNavigation.NavigationTo<ProjectMainViewModel>(result.Path);
            await UserTabViewService.OpenProjectAsync(result.Path);
        }
    }

    [RelayCommand]
    async Task SaveProjectAsync()
    {
        var result = await ProjectService.SaveAsync();
    }
}
