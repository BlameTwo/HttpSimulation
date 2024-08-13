using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpSimulation.Models.Operation
{
    public record class AddFolderInterfaceParam(
        ObservableCollection<InterfaceType> Folders,
        string newFolderName
    );

    public record class AddFolderInterfaceResult(string folderName, InterfaceType BaseFolder);
}
