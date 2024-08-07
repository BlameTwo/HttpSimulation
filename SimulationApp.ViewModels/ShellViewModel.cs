using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Models;
using Microsoft.Extensions.DependencyInjection;
using SimulationApp.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels;

public sealed partial class ShellViewModel:ObservableObject
{
    public ShellViewModel(
        IApplicationSetup<ClientApplication> applicationSetup,
        [FromKeyedServices(Contracts.HostName.MainNavigation)]INavigationService mainNavigation,
        IDialogExtentionService dialogExtentionService,
        IPickersService pickersService)
    {
        ApplicationSetup=applicationSetup;
        MainNavigation=mainNavigation;
        DialogExtentionService=dialogExtentionService;
        PickersService=pickersService;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }
    public INavigationService MainNavigation { get; }
    public IDialogExtentionService DialogExtentionService { get; }
    public IPickersService PickersService { get; }

    [RelayCommand]
    async Task ShowCreateProject()
    {
        await DialogExtentionService.ShowCreateProjectAsync();
    }

    [RelayCommand]
    async Task OpenProjectAsync()
    {
        var picker =  PickersService.GetFileOpenPicker(new string[] {".zip"});
        var result = await picker.PickSingleFileAsync();
        if (result!=null)
        {
            var projcet = await SimulationProjcet.ParseAsync((await result.OpenReadAsync()).AsStreamForRead());
            MainNavigation.NavigationTo<ProjectMainViewModel>(projcet);
        }
    }
}
