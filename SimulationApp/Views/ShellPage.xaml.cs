using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using WinUIExtentions;
using WinUIExtentions.Contracts;
using WinUIExtentions.Controls;
using static WinUIExtentions.Controls.AppTabView;

namespace SimulationApp.Views;

public sealed partial class ShellPage : Page
{
    public ShellPage()
    {
        this.InitializeComponent();
        this.ViewModel = Setup.GetService<ShellViewModel>();
        this.ViewModel.TabViewService.Register(this.tabview);
    }

    public ShellViewModel ViewModel { get; }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //titlebar.Window = ViewModel.ApplicationSetup.Application.MainWindow;
        Setup.GetService<IDialogManager>().RegisterRoot(this.XamlRoot);
        this.SizeChanged += ShellPage_SizeChanged;
    }

    private void ShellPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (tabview is AppTabView view)
        {
            SetDragArea(view.GetTabArea());
        }
    }

    private void SetDragArea(TabAreaLength tabAreaLength)
    {
        var window = ViewModel.ApplicationSetup.Application.MainWindow;
        List<RectInt32> rects = new List<RectInt32>();
        RectInt32 rect = new();
        var ScaleAdjustment = WinUIExtentions.Controls.TitleBar.GetScaleAdjustment(window);
        rect.Y = 0;
        rect.X = (int)((tabAreaLength.x + leftPanel.ActualWidth + 10) * ScaleAdjustment);
        rect.Height = (int)(tabAreaLength.height * ScaleAdjustment);
        rect.Width = (int)(tabAreaLength.width * ScaleAdjustment);
        rects.Add(rect);
        this.ContentRect = rects;
        ViewModel.ApplicationSetup.Application.MainWindow.AppWindow.TitleBar.SetDragRectangles(
            this.ContentRect.ToArray()
        );
    }

    public List<RectInt32> ContentRect
    {
        get { return (List<RectInt32>)GetValue(ContentRectProperty); }
        set { SetValue(ContentRectProperty, value); }
    }

    public static readonly DependencyProperty ContentRectProperty = DependencyProperty.Register(
        "ContentRect",
        typeof(List<RectInt32>),
        typeof(ShellPage),
        new PropertyMetadata(null)
    );

    private void AppTabView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is AppTabView view)
        {
            SetDragArea(view.GetTabArea());
        }
    }
}
