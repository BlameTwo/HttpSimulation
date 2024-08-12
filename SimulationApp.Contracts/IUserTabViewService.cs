using System.Threading.Tasks;
using HttpSimulation.Models;

namespace SimulationApp.Contracts;

public interface IUserTabViewService
{
    public Task OpenProjectAsync(string path);

    public void OpenInterface(InterfaceType type);

    public void OpenHome();

    public void CloseTab(string key);
}
