using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using SimulationApp.Contracts;
using SimulationApp.Contracts.Models;
using System;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class AddInterfaceViewModel : ObservableObject,IContentDialogViewModel<AddInterfaceParam,AddInterfaceResult>
{
    [ObservableProperty]
    AddInterfaceParam data;

    public AddInterfaceResult? Build()
    {
        return new();
    }

    public void Update()
    {
    }
}