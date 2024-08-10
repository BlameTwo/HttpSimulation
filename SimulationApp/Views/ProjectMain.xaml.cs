using SimulationApp.ViewModels;
using SimulationApp.Views.Bases;
using WinUIExtentions;

namespace SimulationApp.Views;

public sealed partial class ProjectMain : ProjectView
{
    public ProjectMain()
    {
        this.InitializeComponent();
        this.ViewModel = Setup.GetService<ProjectMainViewModel>();
    }
}
