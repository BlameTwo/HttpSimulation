using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;

namespace WinUIExtentions.Contracts;

public interface IPageService
{
    public Type GetPage(string key);


    public void RegisterView<View, ViewModel>()
        where View : Page
        where ViewModel : ObservableObject;
}