using HttpSimulation.Models;

namespace SimulationApp.Contracts.Models;

public record RenameParam(InterfaceType type);
public record RenameResult(string id,string newName);