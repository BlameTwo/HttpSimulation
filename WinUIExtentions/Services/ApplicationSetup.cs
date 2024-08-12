using System;
using System.Threading.Tasks;
using H.NotifyIcon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;
using Microsoft.Windows.AppLifecycle;
using WinUIEx;
using WinUIExtentions.Common;

namespace WinUIExtentions.Contracts;

public class ApplicationSetup<App, LauncherPage> : IApplicationSetup<App>
    where App : ClientApplication
    where LauncherPage : Page
{
    public ApplicationSetup(
        IThemeService<App> themeService,
        ILocalSettingsService localSettingsService
    )
    {
        LeftPane = null;
        ThemeService = themeService;
        LocalSettingsService = localSettingsService;
    }

    public App Application { get; private set; }

    public string AppName = "Aria2.Client";

    public TaskbarIcon NotyfiIcon { get; private set; }

    public WindowEx LeftPane;

    public bool IsSystemSetup
    {
        get
        {
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run",
                false
            );

            if (startupKey != null)
            {
                string currentValue = (string)startupKey.GetValue(AppName);

                if (currentValue != null)
                {
                    return true;
                }
            }
            startupKey?.Close();
            return false;
        }
    }

    public AppActivationArguments LauncherArgs { get; private set; }
    public IThemeService<App> ThemeService { get; }
    public ILocalSettingsService LocalSettingsService { get; }

    public async Task LauncherAsync(
        App app,
        Microsoft.Windows.AppLifecycle.AppActivationArguments activatedEventArgs
    )
    {
        this.LauncherArgs = activatedEventArgs;
        await InitAsync(app);
    }

    private async Task InitAsync(App app)
    {
        this.Application = app;
        this.Application.MainWindow = new();
        this.Application.MainWindow.SystemBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop()
        {
            Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base
        };
        this.Application.MainWindow.ExtendsContentIntoTitleBar = true;
        Application.MainWindow.Content = Setup.ServiceProvider.GetService<LauncherPage>();
        this.Application.MainWindow.Activate();
        await ThemeService.InitializeAsync(app);
    }

    public void TryEnqueue(Action action)
    {
        this.Application.MainWindow.DispatcherQueue.TryEnqueue(
            new Microsoft.UI.Dispatching.DispatcherQueueHandler(() => action())
        );
    }

    public void RegisterNotifyIcon(TaskbarIcon icon)
    {
        this.NotyfiIcon = icon;
    }
}
