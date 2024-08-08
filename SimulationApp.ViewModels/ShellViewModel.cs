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

namespace SimulationApp.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    public ShellViewModel(
        IApplicationSetup<ClientApplication> applicationSetup,
        [FromKeyedServices(Contracts.HostName.MainNavigation)] INavigationService mainNavigation,
        IDialogExtentionService dialogExtentionService,
        IPickersService pickersService,
        IProjectService projectService
    )
    {
        ApplicationSetup = applicationSetup;
        MainNavigation = mainNavigation;
        DialogExtentionService = dialogExtentionService;
        PickersService = pickersService;
        ProjectService = projectService;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }
    public INavigationService MainNavigation { get; }
    public IDialogExtentionService DialogExtentionService { get; }
    public IPickersService PickersService { get; }
    public IProjectService ProjectService { get; }

    [RelayCommand]
    async Task ShowCreateProject()
    {
        var dialogResult = await DialogExtentionService.CreateProjectAsync("新项目");
        var result = await dialogResult.project.SaveAsAsync(dialogResult.path);
        if (result != null)
            MainNavigation.NavigationTo<ProjectMainViewModel>(dialogResult.path);
    }

    [RelayCommand]
    async Task OpenProjectAsync()
    {
        var picker = PickersService.GetFileOpenPicker(new string[] { ".zip" });
        var result = await picker.PickSingleFileAsync();
        if (result != null)
        {
            //var projcet = await SimulationProjcet.ParseAsync((await result.OpenReadAsync()).AsStreamForRead());
            MainNavigation.NavigationTo<ProjectMainViewModel>(result.Path);
        }
    }

    [RelayCommand]
    async Task SaveProjectAsync()
    {
        var result = await ProjectService.SaveAsync();
    }
}
