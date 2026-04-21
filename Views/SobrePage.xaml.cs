using JardimSimplesApp.ViewModels;

namespace JardimSimplesApp.Views;

public partial class SobrePage : ContentPage
{
    public SobrePage()
    {
        InitializeComponent();

        // A tela Sobre também usa ViewModel para seguir o padrão MVVM.
        BindingContext = new SobreViewModel();
    }
}
