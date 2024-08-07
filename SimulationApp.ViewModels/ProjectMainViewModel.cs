using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using SimulationApp.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimulationApp.ViewModels;

public sealed partial class ProjectMainViewModel : ObservableRecipient, IRecipient<ReInterfaceName>,IRecipient<RemoveInterface>
{
    public ProjectMainViewModel(IDialogExtentionService dialogExtentionService)
    {
        DialogExtentionService=dialogExtentionService;
        this.IsActive=true;
    }


    [ObservableProperty]
    SimulationProjcet _ProjectData;

    public void SetData(SimulationProjcet project)
    {
        this.ProjectData = project;
        this.Interfaces= new(project.Interfaces);
    }

    [ObservableProperty]
    ObservableCollection<InterfaceType> interfaces;

    [ObservableProperty]
    InterfaceType selectInterface;

    public IDialogExtentionService DialogExtentionService { get; }

    [RelayCommand]
    async Task CreateInterfaceTask()
    {
        var folder = this.Interfaces.Where(x => x.Type == HttpSimulation.Models.Enums.RequestType.Folder).Select(x => x.Name);
        var result = await DialogExtentionService.CreateInterfaceAsync(folder.ToList());
        if (folder.Contains(result.BaseFolder))
        {
            foreach (var item in Interfaces)
            {
                if (item.Type == HttpSimulation.Models.Enums.RequestType.Folder && item.Name == result.BaseFolder)
                {
                    (item as FolderInterface).Interfaces.Add(result.Interface);
                }
            }
            return;
        }
        this.Interfaces.Add(result.Interface);
    }

    [RelayCommand]
    async Task CrateInterfaceFolder()
    {
        //var list = this.Interfaces.Where(x => x.Type == HttpSimulation.Models.Enums.RequestType.Folder && x.Name.Contains("新建文件夹"));
        //if(list==null || list.Count() == 0)
        //{
        //    this.Interfaces.Add(new FolderInterface()
        //    {
        //        ID = Guid.NewGuid().ToString("N").ToUpper(),
        //        Name = "新建文件夹(1)",
        //        Interfaces=new()
        //    });
        //}
        //else
        //{

        //}
        var name = GenerateNextFolderName(this.Interfaces.Select(x => x.Name).ToList());
        this.Interfaces.Add(new FolderInterface()
        {
            ID = Guid.NewGuid().ToString("N").ToUpper(),
            Name = name,
            Interfaces=new()
        });
    }

    public string GenerateNextFolderName(List<string> folders)
    {
        int maxNumber = 0;
        bool hasMatchingFolders = false;

        foreach (var folder in folders)
        {
            // 使用正则表达式来匹配形如 "新建文件夹(n)" 的字符串
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

        // 如果没有找到匹配的文件夹，从1开始计数
        if (!hasMatchingFolders)
        {
            maxNumber = 0;
        }

        // 生成新的文件夹名
        return $"新建文件夹({maxNumber + 1})";
    }

    partial void OnSelectInterfaceChanged(InterfaceType oldValue, InterfaceType newValue)
    {

    }

    [RelayCommand]
    void DFF()
    {

    }

    public async void Receive(ReInterfaceName message)
    {
        var result = await DialogExtentionService.CreateRenameResultAsync(message.Interface);
        if (result == null)
            return;
        ReName(this.Interfaces,result.id, result.newName);
    }

    public void ReName(ObservableCollection<InterfaceType> interfaces,string id,string newName)
    {
        foreach (var iface in interfaces)
        {
            if(iface.ID == id)
            {
                iface.Name = newName;
            }
            if(iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                var list = iface as FolderInterface;
                ReName(list.Interfaces,id,newName);
            }
        }
    }


    public void Remove(IEnumerable<InterfaceType> types,InterfaceType message)
    {
        foreach (var iface in types)
        {
            if (iface.Type == HttpSimulation.Models.Enums.RequestType.Folder)
            {
                if(iface.ID == message.ID)
                {
                    Interfaces.Remove(iface);
                }
                var list = iface as FolderInterface;
                var remove =  RemoveFolder(list.Interfaces,message);
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

    private IEnumerable<InterfaceType>? RemoveFolder(ObservableCollection<InterfaceType> interfaces, InterfaceType message)
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

    public void Receive(RemoveInterface message)
    {
       Remove(this.Interfaces.ToList(),message.Interface); 
    }
}