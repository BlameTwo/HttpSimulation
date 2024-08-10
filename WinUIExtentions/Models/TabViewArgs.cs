using System;
using WinUIExtentions.Contracts.TabView;

namespace WinUIExtentions.Models;

/// <summary>
/// 视图参数
/// </summary>
/// <param name="ViewKey">ViewKey</param>
/// <param name="Header">标题</param>
/// <param name="IsCloseVisibility">是否支持关闭</param>
/// <param name="Icon">图标</param>
/// <param name="ServiceKey">服务Key</param>
public class TabViewArgs<T, Param, VM>
    where T : AppTabItemBase<Param, VM>
{
    public TabViewArgs(string Header, bool IsCloseVisibility, Param param, string PageKey)
    {
        this.Header = Header;
        this.IsCloseVisibility = IsCloseVisibility;
        this.param = param;
        this.PageKey = PageKey;
    }

    public string Header { get; }
    public bool IsCloseVisibility { get; }
    public Param param { get; }
    public string PageKey { get; }
}
