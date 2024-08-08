using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpSimulation.Models;

namespace SimulationApp.Contracts
{
    public interface IContentDialogViewModel<Param, Result>
    {
        public Result? Build();

        public Param Data { get; set; }

        public void Update();
    }
}
