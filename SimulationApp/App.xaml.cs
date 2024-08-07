using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using SimulationApp.ViewModels;
using SimulationApp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinUIExtentions;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;
using WinUIExtentions.Models;

namespace SimulationApp;

public partial class App : ClientApplication
{
    public App()
    {
        ProgramLife.InitService();
        Setup.GetService<IPageService>().RegisterView<ProjectMain, ProjectMainViewModel>();
        this.InitializeComponent();
    }

    protected async override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Instance = AppInstance.FindOrRegisterForKey("Aria2.Client");
        Instance.Activated += Instance_Activated;
        var activatedEventArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
        if (Instance.IsCurrent)
        {
            await Setup.GetService<ILocalSettingsService>()!.InitSetting(new Dictionary<string, object>()
            {
                {AppSettingKey.ThemeColor, 1},
                {AppSettingKey.WallpaperEnable, true },
                {"SetupFlag",true },
                {"Trackers",new List<string>() }
            });
            var application = Setup.GetService<IApplicationSetup<ClientApplication>>();
            await application.LauncherAsync(this, activatedEventArgs);
        }
        else
        {
            await Instance.RedirectActivationToAsync(activatedEventArgs);
            Process.GetCurrentProcess().Kill();
        }
    }

    private void Instance_Activated(object sender, AppActivationArguments e)
    {
    }

    private Window m_window;

    public AppInstance Instance { get; private set; }
}
