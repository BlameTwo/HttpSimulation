﻿using HttpSimulation.Models;
using SimulationApp.Contracts.Models;
using System.Threading.Tasks;
using WinUIExtentions.Contracts;

namespace SimulationApp.Contracts;

public interface IDialogExtentionService
{
    public IDialogManager DialogManager { get; }

    public Task ShowCreateProjectAsync();


    public Task<AddInterfaceResult?> CreateInterfaceAsync(System.Collections.Generic.List<string> list);

    public Task<RenameResult> CreateRenameResultAsync(InterfaceType type);
}