using JardimSimplesApp.Views;

namespace JardimSimplesApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Registramos a rota da tela de formulário para permitir navegação.
            // Routing.RegisterRoute(nameof(FormServicoPage), typeof(FormServicoPage));
            Routing.RegisterRoute("formulario", typeof(Views.FormServicoPage));

        }
    }
}
