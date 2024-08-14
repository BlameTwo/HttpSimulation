using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using SimulationApp.ViewModels.UserControlViewModels;
using WinUIExtentions;
using WinUIExtentions.Contracts.TabView;

namespace SimulationApp.Views.Bases
{
    public class HttpInterfaceView : AppTabItemBase<HttpInterface, HttpInterfaceViewModel>
    {
        public HttpInterfaceView()
        {
            this.ViewModel = Setup.GetService<HttpInterfaceViewModel>();
        }

        public override void SetParam(HttpInterface param)
        {
            this.ViewModel.SetData(param);
        }

        public override void ViewModelChanged() { }
    }
}
