using System.Collections.ObjectModel;
using HttpSimulation.Models;
using HttpSimulation.Models.Enums;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;

namespace HttpSimulation.Services;

partial class ProjectService
{
    public bool AddFolder()
    {
        var name = GenerateNextFolderName(this.Interfaces.Select(x => x.Name).ToList());
        this.Interfaces.Add(
            new FolderInterface()
            {
                ID = Guid.NewGuid().ToString("N").ToUpper(),
                Name = name,
                Interfaces = new()
            }
        );
        return true;
    }

    public bool AddInterface(AddInterfaceResult result)
    {
        var folder = this
            .Interfaces.Where(x => x.Type == RequestType.Folder)
            .Select(x => x.Name)
            .ToList();
        if (folder.Contains(result.BaseFolder))
        {
            foreach (var item in Interfaces)
            {
                if (
                    item.Type == HttpSimulation.Models.Enums.RequestType.Folder
                    && item.Name == result.BaseFolder
                )
                {
                    (item as FolderInterface).Interfaces.Add(result.Interface);
                }
            }
            return false;
        }
        Interfaces.Add(result.Interface);
        return true;
    }

    public void ReName(ObservableCollection<InterfaceType> interfaces, string id, string newName)
    {
        foreach (var iface in interfaces)
        {
            if (iface.ID == id)
            {
                iface.Name = newName;
            }
            if (iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                var list = iface as FolderInterface;
                ReName(list.Interfaces, id, newName);
            }
        }
    }

    public void Remove(IEnumerable<InterfaceType> types, InterfaceType message)
    {
        foreach (var iface in types)
        {
            if (iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                if (iface.ID == message.ID)
                {
                    Interfaces.Remove(iface);
                }
                var list = iface as FolderInterface;
                var remove = RemoveFolder(list.Interfaces, message);
                if (remove == null)
                    continue;
                list.Interfaces = new(remove);
                break;
            }
            if (iface.ID == message.ID)
            {
                Interfaces.Remove(iface);
            }
        }
    }

    private IEnumerable<InterfaceType>? RemoveFolder(
        ObservableCollection<InterfaceType> interfaces,
        InterfaceType message
    )
    {
        foreach (var iface in interfaces)
        {
            var list = iface as FolderInterface;
            foreach (var iface2 in interfaces.ToList())
            {
                if (iface2.ID == message.ID)
                {
                    interfaces.Remove(iface2);
                    return interfaces;
                }
            }
        }
        return null;
    }
}
