using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace WinUIExtentions.Contracts;

public interface IPickersService
{
    public Task<StorageFolder> GetFolderPicker();

    public FileOpenPicker GetFileOpenPicker(string[] strings);

    public FileSavePicker GetFileSavePicker();
}
