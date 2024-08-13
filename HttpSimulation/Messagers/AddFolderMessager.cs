using System.Collections.ObjectModel;
using HttpSimulation.Models;

namespace HttpSimulation.Messagers;

public record AddFolderMessager(ObservableCollection<InterfaceType> BaseFolder);
