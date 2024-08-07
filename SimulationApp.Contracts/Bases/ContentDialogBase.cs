using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace SimulationApp.Contracts.Bases;


public abstract class ContentDialogBase<Param, Result,VM> : UserControl
    where VM : IContentDialogViewModel<Param,Result>
{

    public VM ViewModel
    {
        get { return (VM)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(VM), typeof(ContentDialogBase<Param, Result, VM>), new PropertyMetadata(null));




    public virtual void SetParam(Param param)
    {
        this.ViewModel.Data = param;
        this.ViewModel.Update();
    }


    public virtual Result GetResult()
    {
        return this.ViewModel.Build();
    }
}