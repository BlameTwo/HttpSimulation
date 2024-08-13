using System.Threading.Tasks;
using SimulationApp.Views.Bases;
using WinUIExtentions.Contracts;
using WinUIExtentions.Models;

namespace SimulationApp.Views.UserControls
{
    public sealed partial class HttpInterfaceControl : HttpInterfaceView, ITabViewType
    {
        public HttpInterfaceControl()
        {
            this.InitializeComponent();
            markdown.Config = new CommunityToolkit.Labs.WinUI.MarkdownTextBlock.MarkdownConfig();
        }

        public TabItemType TabItemType => TabItemType.Interface;

        public Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }
    }
}
