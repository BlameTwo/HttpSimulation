using HttpSimulation.Common;
using HttpSimulation.Contracts;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using System.Text;

namespace HttpSimulation.Services;

public sealed partial class ProjectService : IProjectService
{
    public SimulationProjcet CreateProject(string name)
    {
        SimulationProjcet proj = new SimulationProjcet(Guid.NewGuid().ToString("N"));
        proj.ProjectName = name;

        proj.LastEditTime = DateTime.Now;
        return proj;
    }

    public Task<SimulationProjcet?> SaveProjectAsync(SimulationProjcet project,string path)
    {
        return project.SaveAsAsync(path);
    }

    
}
