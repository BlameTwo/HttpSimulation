using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using static System.Net.WebRequestMethods;

namespace SimulationApp.ViewModels.UserControlViewModels;

public partial class HttpInterfaceViewModel : ObservableObject
{
    [ObservableProperty]
    HttpInterface method;

    public void SetData(HttpInterface param)
    {
        this.Method = param;
    }

    [RelayCommand]
    void SetMethod(string method)
    {
        this.Method.Data.HttpMethod = method;
    }
}
