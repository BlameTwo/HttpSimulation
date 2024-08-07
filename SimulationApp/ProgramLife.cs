using HttpSimulation.Contracts;
using HttpSimulation.Services;
using Microsoft.Extensions.DependencyInjection;
using SimulationApp.Contracts;
using SimulationApp.Services;
using SimulationApp.ViewModels;
using SimulationApp.ViewModels.DialogViewModels;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using SimulationApp.Views;
using SimulationApp.Views.Dialogs;
using SimulationApp.Views.Dialogs.InterfaceDialog;
using WinUIExtentions;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;
using WinUIExtentions.Services;

namespace SimulationApp;

public static class ProgramLife
{
    public static void InitService()
    {
        var service = new ServiceCollection()
            .AddSingleton<IApplicationSetup<ClientApplication>, ApplicationSetup<ClientApplication, ShellPage>>()
            .AddSingleton<IThemeService<ClientApplication>, ThemeService<ClientApplication>>()
            .AddSingleton<IPageService, PageService>()
            .AddSingleton<IDialogManager, DialogManager>()
            .AddSingleton<ILocalSettingsService, LocalSettingsService>()
            .AddTransient<IDataFactory, DataFactory>()
            .AddTransient<ShellPage>()
            .AddTransient<ShellViewModel>()
            .AddSingleton<IPickersService, PickersService>()
        #region ViewModel
            .AddTransient<ProjectMainViewModel>()
        #endregion
        #region Dialog
            .AddTransient<NewProjectDialog>()
            .AddTransient<NewProjectViewModel>()

            .AddTransient<AddInterfaceDialog>()
            .AddTransient<AddInterfaceViewModel>()
            .AddTransient<RenameInterfaceViewModel>()
            .AddTransient<RenameInterfaceDialog>()
        #endregion
        #region 应用扩展
            .AddTransient<IDialogExtentionService, DialogExtentionService>()
            .AddKeyedSingleton<INavigationService, MainNavigationService>(HostName.MainNavigation)
        #endregion
        #region 项目
            .AddTransient<IProjectService,ProjectService>()
        #endregion
            .BuildServiceProvider();
        Setup.InitService(service);
    }


}