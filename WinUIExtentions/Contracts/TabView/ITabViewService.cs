using Microsoft.UI.Xaml.Controls;
using WinUIExtentions.Controls;
using WinUIExtentions.Models;

namespace WinUIExtentions.Contracts.TabView;

public interface ITabViewService
{
    AppTabView View { get; set; }

    public void Register(AppTabView view);

    public void Unregister(AppTabView view);
    public void NavigationTo<T, Param, VM>(TabViewArgs<T, Param, VM> args)
        where T : AppTabItemBase<Param, VM>;
    void Close(string key);
}
