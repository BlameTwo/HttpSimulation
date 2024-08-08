using HttpSimulation.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SimulationApp.ViewModels;
using WinUIExtentions;

namespace SimulationApp.Views;

public sealed partial class ProjectMain : Page
{
    public ProjectMain()
    {
        this.InitializeComponent();
        this.ViewModel = Setup.GetService<ProjectMainViewModel>();
    }

    public ProjectMainViewModel ViewModel { get; }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
    }


    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        if(e.Parameter is string project)
        {
            await this.ViewModel.SetDataAsync(project);
        }
        base.OnNavigatedTo(e);
    }
}
