using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Contracts;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;

namespace HttpSimulation.Services;

public sealed partial class ProjectService : ObservableRecipient, IProjectService
{
    public SimulationProjcet CreateProject(string name)
    {
        SimulationProjcet proj = new SimulationProjcet(Guid.NewGuid().ToString("N"));
        proj.ProjectName = name;
        proj.LastEditTime = DateTime.Now;
        return proj;
    }

    public async Task<bool> LoadAsync(string path)
    {
        var project = await SimulationProjcet.ParseAsync(path);
        if (project == null)
        {
            return false;
        }
        this.CurrentProjectFile = path;
        this.CurrentSimulationProject = project;
        if (project.Interfaces != null)
            this.Interfaces = new(project.Interfaces);
        else
            this.Interfaces = new();
        return true;
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

    public bool Load(SimulationProjcet project)
    {
        this.CurrentSimulationProject = project;
        if (project.Interfaces != null)
            this.Interfaces = new(project.Interfaces);
        else
            this.Interfaces = new();
        return true;
    }

    public Task<SimulationProjcet?> SaveProjectAsync(SimulationProjcet project, string path)
    {
        return project.SaveAsAsync(path);
    }

    public async Task<bool> SaveAsync()
    {
        if (this.CurrentProjectFile != null)
        {
            this.CurrentSimulationProject.Interfaces = this.Interfaces;
            if (await this.CurrentSimulationProject.SaveAsAsync(CurrentProjectFile) != null)
            {
                return true;
            }
        }
        return false;
    }

    public bool AddInterface(AddInterfaceResult result)
    {
        var folder = this
            .Interfaces.Where(x => x.Type == Models.Enums.RequestType.Folder)
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
}
