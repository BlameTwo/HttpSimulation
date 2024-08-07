using WinUIExtentions.Contracts;
using WinUIExtentions.Navigations.Bases;

namespace SimulationApp.Services;

public sealed class MainNavigationService : NavigationServiceBase
{
    public MainNavigationService(IPageService pageService) : base(pageService)
    {
    }
}
