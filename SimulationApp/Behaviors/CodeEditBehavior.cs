using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using WinUIEditor;

namespace SimulationApp.Behaviors;

public class CodeEditBehavior : Behavior<CodeEditorControl>
{
    public CodeEditBehavior() { }

    protected override void OnAttached()
    {
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text",
        typeof(string),
        typeof(CodeEditBehavior),
        new PropertyMetadata(null, OnPropertyChanged)
    );

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CodeEditBehavior control)
        {
            if (control.AssociatedObject == null)
                return;
            control.AssociatedObject.Editor.SetText(e.NewValue as string);
        }
    }
}
