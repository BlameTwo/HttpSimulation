using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using WinUIExtentions;
using WinUIExtentions.Contracts;

namespace WinUIExtentions.Contracts;

public class DialogManager : IDialogManager
{
    public XamlRoot Root { get; private set; }

    public ContentDialog _dialog = null;

    public void CloseDialog()
    {
        if (_dialog != null)
        {
            _dialog.Hide();
        }
    }

    public void RegisterRoot(XamlRoot root)
    {
        this.Root = root;
    }

   

    public async Task ShowDialogAsync<T>()
        where T:ContentDialog
    {
        var dialog = Setup.ServiceProvider.GetService<T>();
        if (dialog == null) return;
        dialog.XamlRoot = Root;
        this._dialog = dialog;
        await dialog.ShowAsync();
    }

    public async Task<ContentDialogResult> ShowResultDialogAsync<T>()
        where T:ContentDialog
    {
        var dialog = Setup.ServiceProvider.GetService<T>();
        if (dialog == null) return ContentDialogResult.None;
        dialog.XamlRoot = Root;
        this._dialog = dialog;
        return await _dialog.ShowAsync();
    }


    public async Task ShowDialogAsync<T, Value>(Value type) where T : ContentDialog, IDialogBase<Value>
    {
        var dialog = Setup.ServiceProvider.GetService<T>();
        if (dialog == null) return;
        dialog.XamlRoot = Root;
        this._dialog = dialog;
        dialog.SetData(type);
        await dialog.ShowAsync();
    }
}
