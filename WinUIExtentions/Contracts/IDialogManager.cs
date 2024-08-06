using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace WinUIExtentions.Contracts;

public interface IDialogManager
{
    public XamlRoot Root { get; }

    public void RegisterRoot(XamlRoot root);


    public Task ShowDialogAsync<T, Value>(Value type) where T : ContentDialog, IDialogBase<Value>;

    public Task ShowDialogAsync<T>()
        where T : ContentDialog;

    public Task<ContentDialogResult> ShowResultDialogAsync<T>()
        where T : ContentDialog;
    public void CloseDialog();

}