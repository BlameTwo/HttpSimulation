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

    /// <summary>
    /// 如果选择已经有的目录
    /// </summary>
    public string[] BaseFolders { get; set; }
}
