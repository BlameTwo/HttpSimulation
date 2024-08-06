using SimulationApp.Contracts;
using SimulationApp.Views.Dialogs;
using System.Threading.Tasks;
using WinUIExtentions.Contracts;

namespace SimulationApp.Services;

public class DialogExtentionService : IDialogExtentionService
{
    public DialogExtentionService(IDialogManager dialogManager)
    {
        DialogManager=dialogManager;
    }

    public IDialogManager DialogManager { get; }

    public async Task ShowCreateProjectAsync()
    {
        await DialogManager.ShowDialogAsync<NewProjectDialog>();
    }
}
