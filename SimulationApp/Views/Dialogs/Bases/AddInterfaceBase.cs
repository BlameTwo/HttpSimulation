﻿

using HttpSimulation.Models;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.Contracts.Bases;
using SimulationApp.Contracts.Models;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using WinUIExtentions;

namespace SimulationApp.Views.Dialogs.Bases;

public class AddInterfaceBase : ContentDialogBase<AddInterfaceParam,AddInterfaceResult,AddInterfaceViewModel>
{
    public AddInterfaceBase()
    {
        this.ViewModel  = Setup.GetService<AddInterfaceViewModel>();
    }


}