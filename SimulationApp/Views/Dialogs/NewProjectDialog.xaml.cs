using SimulationApp.Contracts.Bases;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using SimulationApp.Views.Dialogs.Bases;
using WinUIExtentions;

namespace SimulationApp.Views.Dialogs;

public sealed partial class NewProjectDialog : AddProjectBase
{
    public NewProjectDialog()
    {
        this.InitializeComponent();
    }

    private async void AutoSuggestBox_QuerySubmitted(
        Microsoft.UI.Xaml.Controls.AutoSuggestBox sender,
        Microsoft.UI.Xaml.Controls.AutoSuggestBoxQuerySubmittedEventArgs args
    )
    {
        await this.ViewModel.GetSavePath();
    }
}
