using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models.InterfaceTypes.HttpInterfaces;

namespace SimulationApp.ViewModels.HttpViewModels;

public partial class HttpGetParamViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<HttpGetParam> getParams;
}
