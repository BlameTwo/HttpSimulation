using System.Threading.Tasks;
using HttpSimulation.Models;
using SimulationApp.ViewModels;
using WinUIExtentions;
using WinUIExtentions.Contracts;
using WinUIExtentions.Contracts.TabView;
using WinUIExtentions.Models;

namespace SimulationApp.Views.Bases;

public class ProjectView : AppTabItemBase<NavigationToProject, ProjectMainViewModel>, ITabViewType
{
    public ProjectView()
    {
        this.ViewModel = Setup.GetService<ProjectMainViewModel>();
    }

    public TabItemType TabItemType => TabItemType.Project;

    public async Task<bool> SaveAsync()
    {
        return await ViewModel.SaveAsync();
    }

    public override void SetParam(NavigationToProject param)
    {
        this.ViewModel.SetData(param);
    }

    public override void ViewModelChanged() { }
}
