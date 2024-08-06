using H.NotifyIcon;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Threading.Tasks;
using WinUIEx;
using WinUIExtentions.Common;

namespace WinUIExtentions.Contracts;

public interface IApplicationSetup<App>
    where App : ClientApplication
{
    public App Application { get; }

    public Task LauncherAsync(App app, AppActivationArguments activatedEventArgs);

    public void TryEnqueue(Action action);

    public TaskbarIcon NotyfiIcon { get; }
    public AppActivationArguments LauncherArgs { get; }

    public void RegisterNotifyIcon(TaskbarIcon icon);

}
