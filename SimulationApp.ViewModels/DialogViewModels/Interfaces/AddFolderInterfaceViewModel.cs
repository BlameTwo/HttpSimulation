using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.Operation;
using SimulationApp.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class AddFolderInterfaceViewModel
    : ObservableObject,
        IContentDialogViewModel<AddFolderInterfaceParam, AddFolderInterfaceResult>
{
    [ObservableProperty]
    AddFolderInterfaceParam data;

    [ObservableProperty]
    InterfaceType? baseFolder;

    [ObservableProperty]
    ObservableCollection<InterfaceType> _baseFolders;

    [ObservableProperty]
    string folderName;

    public AddFolderInterfaceResult Build()
    {
        if (BaseFolder == null)
        {
            return new AddFolderInterfaceResult(this.FolderName, null);
        }
        return new AddFolderInterfaceResult(this.FolderName, BaseFolder);
    }

    public void Update()
    {
        this.FolderName = Data.newFolderName;
        this.BaseFolders = data.Folders;
    }
}
