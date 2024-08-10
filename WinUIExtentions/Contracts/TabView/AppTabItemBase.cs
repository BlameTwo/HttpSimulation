using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUIExtentions.Contracts.TabView;

public abstract class AppTabItemBase<Param, VM> : UserControl
{
    public VM ViewModel
    {
        get { return (VM)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        "ViewModel",
        typeof(VM),
        typeof(AppTabItemBase<Param, VM>),
        new PropertyMetadata(null, OnPropertyChanged)
    );

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AppTabItemBase<Param, VM> control)
        {
            control.ViewModelChanged();
        }
    }

    public abstract void SetParam(Param param);

    public abstract void ViewModelChanged();
}
