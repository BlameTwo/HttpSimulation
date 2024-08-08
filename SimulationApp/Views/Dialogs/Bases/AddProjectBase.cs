using HttpSimulation.Models.Operation;
using SimulationApp.Contracts.Bases;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using WinUIExtentions;

namespace SimulationApp.Views.Dialogs.Bases;

public class AddProjectBase
    : ContentDialogBase<CreateProjectParam, CreateProjectResult, AddProjectViewModel>
{
    public AddProjectBase()
    {
        this.ViewModel = Setup.GetService<AddProjectViewModel>();
    }
}
