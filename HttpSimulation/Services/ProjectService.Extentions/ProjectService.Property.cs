using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HttpSimulation.Messagers;
using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;

namespace HttpSimulation.Services;

partial class ProjectService
{
    public SimulationProjcet CurrentSimulationProject { get; private set; }

    public string CurrentProjectFile { get; private set; }

    public bool IsEdited { get; private set; }

    [ObservableProperty]
    ObservableCollection<InterfaceType> interfaces;

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
