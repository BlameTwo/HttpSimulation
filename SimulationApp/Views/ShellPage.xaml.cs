using Microsoft.UI.Xaml.Controls;
using SimulationApp.ViewModels;
using System.Collections.Generic;
using Windows.Graphics;
using WinUIExtentions;
using WinUIExtentions.Contracts;
using static SimulationApp.Controls.AppTabView;
using WinUIExtentions.Controls;
using Microsoft.UI.Xaml;
using SimulationApp.Controls;

namespace SimulationApp.Views;

public sealed partial class ShellPage : Page
{
    public ShellPage()
    {
        this.InitializeComponent();
        this.ViewModel = Setup.GetService<ShellViewModel>();
    }

    public ShellViewModel ViewModel { get; }

    private void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        titlebar.Window = ViewModel.ApplicationSetup.Application.MainWindow;
        Setup.GetService<IDialogManager>().RegisterRoot(this.XamlRoot);
    }

    private void SetDragArea(TabAreaLength tabAreaLength)
    {
        var window = ViewModel.ApplicationSetup.Application.MainWindow;
        List<RectInt32> rects = new List<RectInt32>();
        RectInt32 rect = new();
        var ScaleAdjustment = TitleBar.GetScaleAdjustment(window);
        rect.Y = ((int)(titlebar.ActualHeight * ScaleAdjustment));
        rect.X = (int)(tabAreaLength.x * ScaleAdjustment);
        rect.Height = (int)(tabAreaLength.height*ScaleAdjustment);
        rect.Width = (int)(tabAreaLength.width*ScaleAdjustment);
        rects.Add(rect);
        this.ContentRect = rects;
    }

    public List<RectInt32> ContentRect
    {
        get { return (List<RectInt32>)GetValue(ContentRectProperty); }
        set { SetValue(ContentRectProperty, value); }
    }

    public static readonly DependencyProperty ContentRectProperty =
        DependencyProperty.Register("ContentRect", typeof(List<RectInt32>), typeof(ShellPage), new PropertyMetadata(null));

    private void AppTabView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is AppTabView view)
        {
            SetDragArea(view.GetTabArea());
        }
    }
}
