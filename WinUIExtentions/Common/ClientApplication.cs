using Microsoft.UI.Xaml;
using WinUIEx;

namespace WinUIExtentions.Common;

public abstract class ClientApplication:Application
{
    public WindowEx MainWindow { get; set; }
}
