using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using Microsoft.UI.Xaml.Controls;
using SimulationApp.Contracts;
using SimulationApp.Contracts.Bases;
using SimulationApp.ViewModels.DialogViewModels.Interfaces;
using SimulationApp.Views.Dialogs;
using SimulationApp.Views.Dialogs.InterfaceDialog;
using WinUIExtentions;
using WinUIExtentions.Contracts;

namespace SimulationApp.Services;

public class DialogExtentionService : IDialogExtentionService
{
    public DialogExtentionService(IDialogManager dialogManager)
    {
        DialogManager = dialogManager;
    }

    public IDialogManager DialogManager { get; }

    public async Task<Result> ShowResultDialogAsync<T, Param, Result, VM>(Param param)
        where T : ContentDialogBase<Param, Result, VM>
        where VM : IContentDialogViewModel<Param, Result>
    {
        var content = Setup.GetService<T>();
        content.SetParam(param);
        var dialog = new ContentDialog()
        {
            Content = content,
            PrimaryButtonText = "确定",
            SecondaryButtonText = "关闭",
        };
        dialog.XamlRoot = DialogManager.Root;
        if ((await dialog.ShowAsync()) == Microsoft.UI.Xaml.Controls.ContentDialogResult.Primary)
        {
            return content.GetResult();
        }
        return default;
    }

    public async Task<CreateProjectResult?> CreateProjectAsync(string name)
    {
        return await ShowResultDialogAsync<
            NewProjectDialog,
            CreateProjectParam,
            CreateProjectResult,
            AddProjectViewModel
        >(new CreateProjectParam(name));
    }

    public async Task<AddInterfaceResult?> CreateInterfaceAsync(
        ObservableCollection<string> folders
    )
    {
        return await ShowResultDialogAsync<
            AddInterfaceDialog,
            AddInterfaceParam,
            AddInterfaceResult,
            AddInterfaceViewModel
        >(new() { BaseFolder = folders });
    }

    public async Task<RenameResult> CreateRenameResultAsync(InterfaceType type)
    {
        return await ShowResultDialogAsync<
            RenameInterfaceDialog,
            RenameParam,
            RenameResult,
            RenameInterfaceViewModel
        >(new(type));
    }
}
