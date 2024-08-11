using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;

namespace HttpSimulation.Services;

partial class ProjectService
{
    public SimulationProjcet CurrentSimulationProject { get; private set; }

    public string CurrentProjectFile { get; private set; }

    public bool IsEdited { get; private set; }
}
