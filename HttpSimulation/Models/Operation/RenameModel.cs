using HttpSimulation.Models;

namespace HttpSimulation.Models.Operation;

public record RenameParam(InterfaceType type);
public record RenameResult(string id, string newName);