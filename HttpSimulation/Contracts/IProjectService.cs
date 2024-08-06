using HttpSimulation.Models;

namespace HttpSimulation.Contracts;

public interface IProjectService
{
    public SimulationProjcet CreateProject(string name);

    public SimulationProjcet Parse(string path);
}