using HttpSimulation.Models;
using HttpSimulation.Models.InterfaceTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimulationApp.Selector
{
    public class PorjectInterfaceSelector : DataTemplateSelector
    {
        public DataTemplate FolderTemplate { get; set; }

        public DataTemplate DefaultTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is FolderInterface)
            {
                return FolderTemplate;
            }
            return DefaultTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Syncfusion.UI.Xaml.TreeView.TreeViewNode node)
            {
                if (node.Content is FolderInterface)
                {
                    return FolderTemplate;
                }
                return DefaultTemplate;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
