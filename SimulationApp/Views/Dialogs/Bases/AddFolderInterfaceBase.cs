using HttpSimulation.Models.Operation;
using SimulationApp.Contracts.Bases;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using WinUIExtentions;

namespace SimulationApp.Views.Dialogs.Bases;

public partial class AddFolderInterfaceBase
    : ContentDialogBase<
        AddFolderInterfaceParam,
        AddFolderInterfaceResult,
        AddFolderInterfaceViewModel
    >
{
    public AddFolderInterfaceBase()
    {
        this.ViewModel = Setup.GetService<AddFolderInterfaceViewModel>();
    }
}
