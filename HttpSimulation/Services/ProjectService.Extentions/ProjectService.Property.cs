using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;

namespace HttpSimulation.Services;

partial class ProjectService
{
    public SimulationProjcet CurrentSimulationProject { get; private set; }

    public string CurrentProjectFile { get; private set; }

    public bool IsEdited { get; private set; }

    ObservableCollection<InterfaceType> interfaces;

    public ObservableCollection<InterfaceType> Interfaces
    {
        get => interfaces;
        set
        {
            SetProperty(ref interfaces, value);
            OnPropertyChanged();
        }
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

    public string GenerateNextFolderName(List<string> folders)
    {
        int maxNumber = 0;
        bool hasMatchingFolders = false;

        foreach (var folder in folders)
        {
            var match = Regex.Match(folder, @"新建文件夹\((\d+)\)");
            if (match.Success)
            {
                hasMatchingFolders = true;
                int number = int.Parse(match.Groups[1].Value);
                if (number > maxNumber)
                {
                    maxNumber = number;
                }
            }
        }
        if (!hasMatchingFolders)
        {
            maxNumber = 0;
        }

        // 生成新的文件夹名
        return $"新建文件夹({maxNumber + 1})";
    }
}
