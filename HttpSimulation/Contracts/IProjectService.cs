using CommunityToolkit.Mvvm.ComponentModel;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using HttpSimulation.Models.Operation;
using System.Collections.ObjectModel;

namespace HttpSimulation.Contracts;

public interface IProjectService
{
    /// <summary>
    /// 当前项目文件
    /// </summary>
    public SimulationProjcet CurrentSimulationProject { get; }

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

    public ObservableCollection<InterfaceType> Interfaces { get; set; }

    public Task<bool> LoadAsync(string path);

    public bool Load(SimulationProjcet project);

    public void ReName(ObservableCollection<InterfaceType> interfaces, string id, string newName);

    public bool AddInterface(AddInterfaceResult result);

    public bool AddFolder();

    public void Remove(IEnumerable<InterfaceType> types, InterfaceType message);

    public Task<bool> SaveAsync();
}