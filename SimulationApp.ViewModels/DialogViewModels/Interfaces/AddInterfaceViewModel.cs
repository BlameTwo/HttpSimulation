using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using SimulationApp.Contracts;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class AddInterfaceViewModel
    : ObservableObject,
        IContentDialogViewModel<AddInterfaceParam, AddInterfaceResult>
{
    [ObservableProperty]
    AddInterfaceParam data;

    [ObservableProperty]
    ObservableCollection<string> folders;

    [ObservableProperty]
    string selectFolder;

    [ObservableProperty]
    string interfaceName;

    public AddInterfaceResult? Build()
    {
        var folder = this.SelectFolder;
        if (string.IsNullOrWhiteSpace(folder))
        {
            return new AddInterfaceResult()
            {
                Interface = new HttpInterface()
                {
                    ID = Guid.NewGuid().ToString("N").ToUpper(),
                    Name = this.InterfaceName,
                    Method = "POST",
                }
            };
        }
        else
        {
            var fillter = this.Folders.Where(x => x == SelectFolder);
            if (fillter == null || fillter.Count() == 0)
            {
                return new AddInterfaceResult()
                {
                    Interface = new FolderInterface()
                    {
                        ID = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = SelectFolder,
                        Interfaces = new ObservableCollection<InterfaceType>()
                        {
                            new HttpInterface()
                            {
                                ID = Guid.NewGuid().ToString("N").ToUpper(),
                                Name = this.InterfaceName,
                                Method = "POST",
                            }
                        }
                    }
                };
            }
            else
            {
                return new AddInterfaceResult()
                {
                    BaseFolders = [SelectFolder],
                    Interface = new HttpInterface()
                    {
                        ID = Guid.NewGuid().ToString("N").ToUpper(),
                        Name = this.InterfaceName,
                        Method = "POST",
                    }
                };
            }
        }
    }

    public InterfaceType? GetFirstFolder(string baseFolder, string createFolder)
    {
        if (baseFolder != createFolder)
        {
            return new FolderInterface()
            {
                ID = Guid.NewGuid().ToString("N").ToUpper(),
                Name = createFolder,
            };
        }
        return null;
    }

    public void Update()
    {
        this.Folders = this.Data.BaseFolder;
    }
}
