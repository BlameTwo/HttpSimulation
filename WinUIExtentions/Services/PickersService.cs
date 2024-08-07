using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinUIExtentions.Common;
using WinUIExtentions.Contracts;

namespace WinUIExtentions.Services;

public class PickersService : IPickersService
{
    public PickersService(IApplicationSetup<ClientApplication> applicationSetup)
    {
        ApplicationSetup = applicationSetup;
    }

    public IApplicationSetup<ClientApplication> ApplicationSetup { get; }

    public FileOpenPicker GetFileOpenPicker(string[] strings)
    {
        FileOpenPicker openPicker = new FileOpenPicker();
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(ApplicationSetup.Application.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
        foreach (var item in strings)
        {
            openPicker.FileTypeFilter.Add(item);
        }
        return openPicker;
    }

    public FileSavePicker GetFileSavePicker()
    {
        throw new NotImplementedException();
    }

    public async Task<StorageFolder> GetFolderPicker()
    {
        FolderPicker openPicker = new FolderPicker();
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(ApplicationSetup.Application.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        return await openPicker.PickSingleFolderAsync();
    }
}
