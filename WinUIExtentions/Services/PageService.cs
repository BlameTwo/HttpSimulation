﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using WinUIExtentions.Contracts;

namespace WinUIExtentions.Contracts;

public sealed partial class PageService:IPageService
{
    private readonly Dictionary<string, Tuple<Type, Type>> _pages;

    public PageService()
    {
        _pages = new();

    }

    public Type GetPage(string key)
    {
        _pages.TryGetValue(key, out var page);
        if(page is null)
        {
            return null;
        }
        return page.Item1;
    }

    public void RegisterView<View, ViewModel>()
        where View : Page
        where ViewModel : ObservableObject =>
        _pages.Add(typeof(ViewModel).FullName, new(typeof(View), typeof(ViewModel)));
}
