using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIExtentions.Helper;

namespace WinUIExtentions.Controls;

public class AppTabView : TabView
{
    public AppTabView()
    {
        DefaultStyleKey = typeof(AppTabView);
    }

    public Visibility TabVisibility
    {
        get { return (Visibility)GetValue(TabVisibilityProperty); }
        set { SetValue(TabVisibilityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TabVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TabVisibilityProperty = DependencyProperty.Register(
        "TabVisibility",
        typeof(Visibility),
        typeof(AppTabView),
        new PropertyMetadata(null)
    );

    public TabAreaLength GetTabArea()
    {
        Border Area = (Border)ElementChildHelper.FindChildByName(this, "TabViewArea");
        return new(Area.ActualHeight, Area.ActualWidth, ActualWidth - Area.ActualWidth);
    }

    /// <summary>
    /// 不包含Y轴的拖动范围
    /// </summary>
    /// <param name="height">高度</param>
    /// <param name="width">宽度</param>
    /// <param name="x">自窗口的X轴位置</param>
    public record TabAreaLength(double height, double width, double x);
}
