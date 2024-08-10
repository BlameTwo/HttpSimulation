using HttpSimulation.Models;
using SimulationApp.ViewModels;
using WinUIExtentions;
using WinUIExtentions.Contracts.TabView;

namespace SimulationApp.Views.Bases;

public class ProjectView : AppTabItemBase<NavigationToProject, ProjectMainViewModel>
{
    public ProjectView()
    {
        this.ViewModel = Setup.GetService<ProjectMainViewModel>();
    }

    public override void SetParam(NavigationToProject param)
    {
        this.ViewModel.SetData(param);
    }

    public override void ViewModelChanged() { }
}
