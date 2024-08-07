using SimulationApp.Contracts.Bases;
using SimulationApp.Contracts.Models;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using WinUIExtentions;

namespace SimulationApp.Views.Dialogs.Bases;

public class RenameInterfaceBase: ContentDialogBase<RenameParam,RenameResult, RenameInterfaceViewModel>
{
    public RenameInterfaceBase()
    {
        this.ViewModel = Setup.GetService<RenameInterfaceViewModel>();
    }



    public override void SetParam(RenameParam param)
    {
        base.SetParam(param);
    }
}