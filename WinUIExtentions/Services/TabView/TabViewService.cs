using System;
using System.Reflection.PortableExecutable;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIExtentions.Contracts;
using WinUIExtentions.Contracts.TabView;
using WinUIExtentions.Controls;
using WinUIExtentions.Models;

namespace WinUIExtentions.Services.TabView;

public sealed class TabViewService : ITabViewService
{
    public AppTabView View { get; set; }

    public void NavigationTo<T, Param, VM>(TabViewArgs<T, Param, VM> args)
        where T : AppTabItemBase<Param, VM>
    {
        if (GetOwnerView(args.PageKey, out var value))
        {
            var t = Setup.GetService<T>();
            t.SetParam(args.param);
            var item = new TabViewItem()
            {
                Content = t,
                Header = args.Header,
                Tag = args.PageKey,

                IsClosable = args.IsCloseVisibility
            };
            this.View.TabItems.Add(item);
            this.View.SelectedIndex = this.View.TabItems.Count - 1;
        }
        else
        {
            this.View.SelectedItem = value;
        }
    }

    public bool GetOwnerView(string key, out object view)
    {
        foreach (var item in View.TabItems)
        {
            if (item is TabViewItem viewitem)
            {
                if (viewitem.Tag != null && viewitem.Tag.ToString() == key)
                {
                    view = viewitem;
                    return false;
                }
            }
        }
        view = null;
        return true;
    }

    public void Register(AppTabView view)
    {
        this.View = view;
        this.View.TabCloseRequested += View_TabCloseRequested;
    }

    private async void View_TabCloseRequested(
        Microsoft.UI.Xaml.Controls.TabView sender,
        TabViewTabCloseRequestedEventArgs args
    )
    {
        if (args.Item is TabViewItem viewitem)
        {
            if (viewitem.Content is IEditControl type)
            {
                type.Save();
                type.Disponse();

                this.View.TabItems.Remove(args.Item);
            }
        }
    }

    public void Unregister(AppTabView view)
    {
        this.View.TabCloseRequested -= View_TabCloseRequested;
        this.View = null;
        GC.Collect();
    }

    public void Close(string key)
    {
        if (!GetOwnerView(key, out var item))
        {
            if (item is IEditControl type)
            {
                type.Save();
                type.Disponse();
                this.View.TabItems.Remove(item);
            }
        }
    }

    public bool UpdateHeader(string key, string newHeader)
    {
        foreach (var item in View.TabItems)
        {
            if (item is TabViewItem viewitem)
            {
                if (viewitem.Tag != null && viewitem.Tag.ToString() == key)
                {
                    viewitem.Header = newHeader;
                    return true;
                }
            }
        }
        return false;
    }

    public void UpDateProgressRing(string key, Visibility visibility)
    {
        foreach (var item in View.TabItems)
        {
            if (item is not TabViewItem viewitem)
                return;
            if (viewitem.Tag != null && viewitem.Tag.ToString() == key)
            {
                WinUIExtentions.Property.TabView.SetTabProgressRingAction(viewitem, visibility);
            }
        }
    }

    public void UpDateIcon(string key, IconSource source)
    {
        foreach (var item in View.TabItems)
        {
            if (item is not TabViewItem viewitem)
                return;
            if (viewitem.Tag != null && viewitem.Tag.ToString() == key)
            {
                viewitem.IconSource = source;
                break;
            }
        }
    }
}
