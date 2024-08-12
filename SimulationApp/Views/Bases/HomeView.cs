using System.Threading.Tasks;
using HttpSimulation.Models;
using SimulationApp.ViewModels;
using WinUIExtentions;
using WinUIExtentions.Contracts;
using WinUIExtentions.Contracts.TabView;
using WinUIExtentions.Models;

namespace SimulationApp.Views.Bases;

public class HomeView : AppTabItemBase<NavigationToHome, HomeViewModel>
{
    public HomeView()
    {
        this.ViewModel = Setup.GetService<HomeViewModel>();
    }

    public TabItemType TabItemType => TabItemType.Project;

    public override void SetParam(NavigationToHome param)
    {
        this.ViewModel.SetData(param);
    }

    public override void ViewModelChanged() { }
}
