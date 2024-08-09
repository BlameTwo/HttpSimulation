using System.Collections.ObjectModel;
using HttpSimulation.Models;

namespace HttpSimulation.Models.Operation;

public class AddInterfaceParam
{
    public ObservableCollection<string> BaseFolder { get; set; }
}

public class AddInterfaceResult
{
    public InterfaceType Interface { get; set; }

    public string BaseFolder { get; set; }
}
