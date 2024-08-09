using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using WinUIExtentions.Contracts;

namespace SimulationApp.Contracts;

public interface IDialogExtentionService
{
    public IDialogManager DialogManager { get; }

    public Task<AddInterfaceResult?> CreateInterfaceAsync(ObservableCollection<string> list);

    public Task<CreateProjectResult?> CreateProjectAsync(string name);

    public Task<RenameResult> CreateRenameResultAsync(InterfaceType type);
}
