using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.Operation;
using SimulationApp.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class RenameInterfaceViewModel
    : ObservableObject,
        IContentDialogViewModel<RenameParam, RenameResult>
{
    [ObservableProperty]
    public RenameParam data;

    [ObservableProperty]
    string _name;

    public RenameResult? Build()
    {
        if (!string.IsNullOrWhiteSpace(Name))
            new RenameResult(Data.type.ID, this.Name);
        return null;
    }

    public void Update()
    {
        this.Name = Data.type.Name;
    }
}
