using System.Threading.Tasks;
using HttpSimulation.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimulationApp.Contracts;

public interface IUserTabViewService
{
    public Task OpenProjectAsync(string path);

    public void OpenInterface(InterfaceType type);

    public void OpenHome();

    public void UpdateHeader(string key, string header);

    public void UpDateProgressRing(string key, Visibility visibility);

    public void UpDateIcon(string key, IconSource source);

    public void CloseTab(string key);
}
