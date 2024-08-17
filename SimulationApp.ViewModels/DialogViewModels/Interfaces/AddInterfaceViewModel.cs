using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using SimulationApp.Contracts;

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
            var id = Guid.NewGuid().ToString("N").ToUpper();
            return new AddInterfaceResult()
            {
                Interface = new HttpInterface()
                {
                    ID = id,
                    Name = this.InterfaceName,
                    Data = new() { HttpMethod = "POST", },
                    BodyData = new()
                    {
                        FromUrlencode = new(),
                        CookieData = new(),
                        FromData = new(),
                        GetParams = new()
                    }
                }
            };
        }
        else
        {
            var fillter = this.Folders.Where(x => x == SelectFolder);
            if (fillter == null || fillter.Count() == 0)
            {
                var interfaceId = Guid.NewGuid().ToString("N").ToUpper();
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
                                ID = interfaceId,
                                Name = this.InterfaceName,
                                Data = new() { HttpMethod = "POST", },
                                BodyData = new()
                                {
                                    FromUrlencode = new(),
                                    CookieData = new(),
                                    FromData = new(),
                                    GetParams = new()
                                }
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
