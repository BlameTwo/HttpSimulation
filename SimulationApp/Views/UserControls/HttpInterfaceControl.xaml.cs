using System;
using System.Threading.Tasks;
using HttpSimulation.Services;
using Microsoft.UI.Xaml.Data;
using SimulationApp.Views.Bases;
using WinUIEditor;
using WinUIExtentions.Contracts;
using WinUIExtentions.Models;

namespace SimulationApp.Views.UserControls
{
    public sealed partial class HttpInterfaceControl : HttpInterfaceView, IEditControl
    {
        public HttpInterfaceControl()
        {
            this.InitializeComponent();
            this.Loaded += HttpInterfaceControl_Loaded;
        }

        private void HttpInterfaceControl_Loaded(
            object sender,
            Microsoft.UI.Xaml.RoutedEventArgs e
        ) { }

        private void jsonEdit_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.jsonEdit.Editor.SetText(this.ViewModel.BodyViewModel.JsonStr);
            jsonEdit.Editor.Modified += Json_EditerModified;
        }

        private void jsonEdit_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ((CodeEditorControl)sender).Editor.Modified -= Json_EditerModified;
        }

        private void Json_EditerModified(
            WinUIEditor.Editor sender,
            WinUIEditor.ModifiedEventArgs args
        )
        {
            this.ViewModel.BodyViewModel.JsonStr = sender.GetText(long.MaxValue);
        }

        private void TextEdit_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.textEdit.Editor.SetText(this.ViewModel.BodyViewModel.RawStr);
            this.textEdit.Editor.Modified += Editor_Modified;
        }

        private void Editor_Modified(Editor sender, ModifiedEventArgs args)
        {
            this.ViewModel.BodyViewModel.RawStr = sender.GetText(long.MaxValue);
        }

        private void TextEdit_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            //this.textEdit.Editor.Modified -= Editor_Modified;

            ((CodeEditorControl)sender)
                .Editor
                .Modified -= Editor_Modified;
        }

        private void XmlEdit_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            //this.xmlEdit.Editor.Modified -= Editor_Modified1;

            ((CodeEditorControl)sender)
                .Editor
                .Modified -= Editor_Modified1;
        }

        private void XmlEdit_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.xmlEdit.Editor.SetText(this.ViewModel.BodyViewModel.XmlStr);
            this.xmlEdit.Editor.Modified += Editor_Modified1;
        }

        private void Editor_Modified1(Editor sender, ModifiedEventArgs args)
        {
            this.ViewModel.BodyViewModel.XmlStr = sender.GetText(long.MaxValue);
        }

        public TabItemType TabItemType => TabItemType.Interface;

        public bool Save()
        {
            return this.ViewModel.SaveData();
        }

        public void Disponse()
        {
            this.Loaded -= HttpInterfaceControl_Loaded;
            if (jsonEdit != null)
            {
                jsonEdit.Loaded -= jsonEdit_Loaded;
                jsonEdit.Unloaded -= jsonEdit_Unloaded;
            }
            if (xmlEdit != null)
            {
                xmlEdit.Unloaded -= XmlEdit_Unloaded;
                xmlEdit.Loaded -= XmlEdit_Loaded;
            }
            if (textEdit != null)
            {
                textEdit.Loaded -= TextEdit_Loaded;
                textEdit.Unloaded -= TextEdit_Unloaded;
            }
            this.Bindings.StopTracking();
            this.ViewModel.Data.Data.GetParams?.Clear();
            this.ViewModel.Data.Data.HeaderCookies?.Clear();
            this.ViewModel.Data = null;
            this.ViewModel = null;
            GC.Collect();
        }
    }
}
