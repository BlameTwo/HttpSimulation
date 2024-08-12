using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Contracts;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;

namespace HttpSimulation.Services;

public sealed partial class ProjectService : ObservableObject, IProjectService
{
    private bool disposedValue;

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
        if (project.Interfaces == null)
        {
            this.Interfaces = new();
        }
        else
        {
            this.Interfaces = new(project.Interfaces);
        }
        return true;
    }

    public bool Load(SimulationProjcet project)
    {
        this.CurrentSimulationProject = project;
        if (project.Interfaces == null)
        {
            this.Interfaces = new();
        }
        else
        {
            this.Interfaces = new(project.Interfaces);
        }
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
            if (await this.CurrentSimulationProject.SaveAsAsync(CurrentProjectFile) != null)
            {
                return true;
            }
        }
        return false;
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) { }
            GC.Collect();
            disposedValue = true;
        }
    }

    // ~ProjectService()
    // {
    //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
