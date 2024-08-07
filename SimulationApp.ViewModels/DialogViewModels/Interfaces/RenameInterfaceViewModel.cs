using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using SimulationApp.Contracts;
using SimulationApp.Contracts.Models;
using System;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class RenameInterfaceViewModel:ObservableObject, IContentDialogViewModel<RenameParam,RenameResult>
{

    [ObservableProperty]
    public RenameParam data;

    [ObservableProperty]
    string _name;



    public RenameResult? Build()
    {
        if(!string.IsNullOrWhiteSpace(Name))
            return new RenameResult(Data.type.ID,this.Name);
        return null;
    }

    public void Update()
    {
        this.Name = Data.type.Name;
    }
}