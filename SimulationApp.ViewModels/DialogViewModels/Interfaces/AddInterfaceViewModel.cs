using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using SimulationApp.Contracts;
using SimulationApp.Contracts.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WinUIExtentions.Contracts;

namespace SimulationApp.ViewModels.DialogViewModels.Interfaces;

public partial class AddInterfaceViewModel : ObservableObject,IContentDialogViewModel<AddInterfaceParam,AddInterfaceResult>
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
            var fillter = this.Folders.Where(x => x==SelectFolder);
            if(fillter==null || fillter.Count() ==0)
            {
                return new()
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
                    BaseFolder = SelectFolder,
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

    public void Update()
    {
        this.Folders = this.Data.BaseFolder;
    }
}