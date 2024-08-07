using HttpSimulation.Models;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.Contracts;
using SimulationApp.Contracts.Bases;
using SimulationApp.Contracts.Models;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using SimulationApp.Views.Dialogs;
using SimulationApp.Views.Dialogs.InterfaceDialog;
using System;
using System.Threading.Tasks;
using WinUIExtentions;
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


    public async Task<Result> ShowResultDialogAsync<T,Param,Result,VM>(Param param)
        where T : ContentDialogBase<Param,Result,VM>
        where VM: IContentDialogViewModel<Param,Result>
    {
        var content = Setup.GetService<T>();
        content.SetParam(param);
        var dialog = new ContentDialog()
        {
            Content = content,
            PrimaryButtonText ="确定",
            SecondaryButtonText="关闭"
        };
        dialog.XamlRoot = DialogManager.Root;
        
        if((await dialog.ShowAsync()) == Microsoft.UI.Xaml.Controls.ContentDialogResult.Primary)
        {
            return content.GetResult();
        }
        return default;
    }

    public async Task<AddInterfaceResult?> CreateInterfaceAsync()
    {
        return await ShowResultDialogAsync<AddInterfaceDialog, AddInterfaceParam,AddInterfaceResult,AddInterfaceViewModel>(new());
    }

    public async Task<RenameResult> CreateRenameResultAsync(InterfaceType type)
    {
        return await ShowResultDialogAsync<RenameInterfaceDialog, RenameParam, RenameResult,RenameInterfaceViewModel>(new(type));
    }

}
