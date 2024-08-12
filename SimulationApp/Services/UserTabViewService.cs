﻿using System;
using System.Threading.Tasks;
using HttpSimulation.Models;
using SimulationApp.Contracts;
using SimulationApp.ViewModels;
using SimulationApp.ViewModels.UserControlViewModels;
using SimulationApp.Views;
using SimulationApp.Views.UserControls;
using WinUIExtentions.Contracts.TabView;

namespace SimulationApp.Services;

public class UserTabViewService : IUserTabViewService
{
    public UserTabViewService(ITabViewService tabViewService)
    {
        TabViewService = tabViewService;
    }

    public ITabViewService TabViewService { get; }

    public async Task OpenProjectAsync(string path)
    {
        var project = await SimulationProjcet.ParseAsync(path);
        if (project == null)
            return;
        TabViewService.NavigationTo(
            new WinUIExtentions.Models.TabViewArgs<
                ProjectMain,
                NavigationToProject,
                ProjectMainViewModel
            >(
                project.ProjectName,
                true,
                new NavigationToProject(project, path),
                typeof(ProjectMain).FullName + project.ID
            )
        );
    }

    public void OpenHome()
    {
        TabViewService.NavigationTo(
            new WinUIExtentions.Models.TabViewArgs<Home, NavigationToHome, HomeViewModel>(
                "首页",
                false,
                new(),
                "Home"
            )
        );
    }

    public void OpenInterface(InterfaceType type)
    {
        if (type.Type == HttpSimulation.Models.Enums.RequestType.Http)
        {
            TabViewService.NavigationTo(
                new WinUIExtentions.Models.TabViewArgs<
                    HttpInterfaceControl,
                    InterfaceType,
                    HttpInterfaceViewModel
                >(type.Name, true, type, type.Name + type.ID)
            );
        }
    }

    public void CloseTab(string key)
    {
        this.TabViewService.Close(key);
    }
}
