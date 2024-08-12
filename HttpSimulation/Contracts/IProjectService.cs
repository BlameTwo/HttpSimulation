using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;

namespace HttpSimulation.Contracts;

public interface IProjectService : IDisposable
{
    /// <summary>
    /// 当前项目文件
    /// </summary>
    public SimulationProjcet CurrentSimulationProject { get; }

    public ObservableCollection<InterfaceType> Interfaces { get; set; }

    /// <summary>
    /// 当前项目文件存储地址
    /// </summary>
    public string CurrentProjectFile { get; }

    /// <summary>
    /// 项目文件是否更改
    /// </summary>
    public bool IsEdited { get; }

    /// <summary>
    /// 创建项目
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SimulationProjcet CreateProject(string name);

    public Task<bool> LoadAsync(string path);

    public bool Load(SimulationProjcet project);

    public void Remove(IEnumerable<InterfaceType> types, InterfaceType message);
    public Task<bool> SaveAsync();
    string GenerateNextFolderName(List<string> list);
    void ReName(ObservableCollection<InterfaceType> interfaces, string id, string newName);
}
