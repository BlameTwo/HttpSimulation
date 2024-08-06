using HttpSimulation.Contracts;
using HttpSimulation.Services;
using Microsoft.Extensions.DependencyInjection;
using SimulationApp.Contracts;
using SimulationApp.Services;
using SimulationApp.ViewModels;
using SimulationApp.ViewModels.DialogViewModels;
using SimulationApp.Views;
using SimulationApp.Views.Dialogs;
using WinUIExtentions;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;

namespace SimulationApp;

public static class ProgramLife
{
    public static void InitService()
    {
        var service  = new ServiceCollection()
            .AddSingleton<IApplicationSetup<ClientApplication>, ApplicationSetup<ClientApplication, ShellPage>>()
            .AddSingleton<IThemeService<ClientApplication>, ThemeService<ClientApplication>>()
            .AddSingleton<IPageService, PageService>()
            .AddSingleton<IDialogManager, DialogManager>()
            .AddSingleton<ILocalSettingsService, LocalSettingsService>()
            .AddTransient<IDataFactory, DataFactory>()
            .AddTransient<ShellPage>()
            .AddTransient<ShellViewModel>()
        #region Dialog
            .AddTransient<NewProjectDialog>()
            .AddTransient<NewProjectViewModel>()
        #endregion
        #region 应用扩展
            .AddTransient<IDialogExtentionService, DialogExtentionService>()
        #endregion
        #region 项目
            .AddTransient<IProjectService,ProjectService>()
        #endregion
            .BuildServiceProvider();
        Setup.InitService(service);
    }


}