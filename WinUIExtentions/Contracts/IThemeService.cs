﻿
using Microsoft.UI.Xaml;
using System.Threading.Tasks;
using WinUIExtentions.Common;


namespace WinUIExtentions.Contracts;

public interface IThemeService<T>
    where T: ClientApplication
{
    ElementTheme Theme { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    Task InitializeAsync(T app);

    /// <summary>
    /// 设置主题
    /// </summary>
    /// <param name="theme"></param>
    /// <returns></returns>
    Task SetThemeAsync(ElementTheme theme);

    /// <summary>
    /// 初始化设置主题
    /// </summary>
    /// <returns></returns>
    Task SetRequestedThemeAsync();
}
