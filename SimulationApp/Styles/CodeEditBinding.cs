using System;
using Microsoft.UI.Xaml;
using WinUIEditor;

namespace SimulationApp.Styles;

public class CodeEditBinding
{
    public static string GetText(DependencyObject obj)
    {
        var value = (string)obj.GetValue(TextProperty);
        //return value;
        if (obj is CodeEditorControl control)
        {
            var res = control.Editor.GetText(control.Editor.Length);
            return res;
        }
        return "";
    }

    public static void SetText(DependencyObject obj, string value)
    {
        obj.SetValue(TextProperty, value);
        if (obj is CodeEditorControl control)
        {
            control.Editor.SetText(value);
        }
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
        "Text",
        typeof(string),
        typeof(CodeEditBinding),
        new PropertyMetadata(null, OnPropertyChanged)
    );

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CodeEditorControl control)
        {
            control.Editor.SetText(e.NewValue as string);
        }
    }
}
