using System.Threading.Tasks;
using HttpSimulation.Services;
using SimulationApp.Contracts;
using SimulationApp.Views.Bases;
using WinUIExtentions.Contracts;
using WinUIExtentions.Models;

namespace SimulationApp.Views.UserControls
{
    public sealed partial class HttpInterfaceControl : HttpInterfaceView, ITabViewType, IEditControl
    {
        public HttpInterfaceControl()
        {
            this.InitializeComponent();
        }

        public TabItemType TabItemType => TabItemType.Interface;

        public Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }

        public bool Save()
        {
            return this.ViewModel.SaveData();
        }
    }
}
