using System.Threading.Tasks;
using WinUIExtentions.Contracts;

namespace SimulationApp.Contracts;

public interface IDialogExtentionService
{
    public IDialogManager DialogManager { get; }

    public Task ShowCreateProjectAsync();
}