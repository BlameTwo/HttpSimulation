using Microsoft.UI.Xaml.Controls;
using SimulationApp.ViewModels.DialogViewModels;
using WinUIExtentions;
using WinUIExtentions.Contracts;


namespace SimulationApp.Views.Dialogs;

public sealed partial class NewProjectDialog : ContentDialog,IDialogBase<string>
{
    public NewProjectDialog()
    {
        this.InitializeComponent();
        this.ViewModel = Setup.GetService<NewProjectViewModel>();
    }

    public NewProjectViewModel ViewModel { get; }

    public void SetData(string data)
    {

    }
}
