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
using System.Threading.Tasks;

namespace SimulationApp.ViewModels;

public sealed partial class ProjectMainViewModel : ObservableRecipient,IRecipient<ReinterfaceName>
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
        var result = await DialogExtentionService.CreateInterfaceAsync();
    }



    partial void OnSelectInterfaceChanged(InterfaceType oldValue, InterfaceType newValue)
    {

    }

    [RelayCommand]
    void DFF()
    {

    }

    public async void Receive(ReinterfaceName message)
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
}