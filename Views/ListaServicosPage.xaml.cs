using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using JardimSimplesApp.ViewModels;

namespace JardimSimplesApp.Views
{
    public partial class ListaServicosPage : ContentPage
    {

        // Propriedade estática usada para passar o serviço entre páginas
        public static ServicoJardinagem? ServicoParaEditar { get; set; }

 
        public ListaServicosPage()
        {
            InitializeComponent();

            // Carrega os dados iniciais só uma vez
            ServicoRepository.CarregarDadosIniciais();

            // Liga a lista visual à coleção do repositório
            ServicosCollectionView.ItemsSource = ServicoRepository.Servicos;
        }

  
        // Abre a tela para cadastrar novo serviço
        private async void OnNovoServicoClicked(object sender, EventArgs e)
        {
            ServicoParaEditar = null; // novo serviço, sem dados
            await Shell.Current.GoToAsync("formulario");
        }

        private async void OnEditarServicoClicked(object sender, EventArgs e)
        {
            var servicoSelecionado = ServicosCollectionView.SelectedItem as ServicoJardinagem;

            if (servicoSelecionado == null)
            {
                await DisplayAlert("Aviso", "Selecione um serviço para editar.", "OK");
                return;
            }

            // Clona para editar sem mexer direto no item da lista
            ServicoParaEditar = servicoSelecionado.Clonar();

            await Shell.Current.GoToAsync("formulario");
        }

        private async void OnExcluirServicoClicked(object sender, EventArgs e)
        {
            var servicoSelecionado = ServicosCollectionView.SelectedItem as ServicoJardinagem;

            if (servicoSelecionado == null)
            {
                await DisplayAlert("Aviso", "Selecione um serviço para excluir.", "OK");
                return;
            }

            bool confirmar = await DisplayAlert(
                "Confirmar",
                $"Deseja excluir o serviço de {servicoSelecionado.Cliente}?",
                "Sim",
                "Não");

            if (confirmar)
            {
                ServicoRepository.Remover(servicoSelecionado);
            }
        }



        // Quando a página reaparece, atualiza a lista e limpa a seleção
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Garante que a CollectionView continue ligada à coleção
            ServicosCollectionView.ItemsSource = ServicoRepository.Servicos;
            ServicosCollectionView.ItemsSource = null;
        }
    }
}