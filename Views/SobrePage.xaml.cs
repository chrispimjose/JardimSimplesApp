using JardimSimplesApp.ViewModels;

namespace JardimSimplesApp.Views;

public partial class SobrePage : ContentPage
{
    // O construtor da página Sobre é onde a inicialização ocorre. Ele chama o método InitializeComponent(),
    public SobrePage()
    {
        InitializeComponent();

        // A tela Sobre também usa ViewModel para seguir o padrão MVVM.
        BindingContext = new SobreViewModel();
    }
}
