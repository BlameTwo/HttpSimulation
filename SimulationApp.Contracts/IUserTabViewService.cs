using System.Threading.Tasks;

namespace SimulationApp.Contracts;

public interface IUserTabViewService
{
    public Task OpenProjectAsync(string path);
}
