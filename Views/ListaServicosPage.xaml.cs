using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using JardimSimplesApp.ViewModels;

namespace JardimSimplesApp.Views
{
    public partial class ListaServicosPage : ContentPage
    {

        // Propriedade estática usada para passar o serviço entre páginas
        public static ServicoJardinagem? ServicoParaEditar { get; set; }

        /// Campo para armazenar o serviço selecionado
        private ServicoJardinagem? _servicoSelecionado;


        // Construtor da página
        public ListaServicosPage()
        {
            InitializeComponent();

            // Carrega os dados iniciais só uma vez
            ServicoRepository.CarregarDadosIniciais();

            // Liga a lista visual à coleção do repositório
            ServicosCollectionView.ItemsSource = ServicoRepository.Servicos;
        }

        // Evento para atualizar o serviço selecionado quando o usuário muda a seleção
        private void OnServicoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _servicoSelecionado = e.CurrentSelection.FirstOrDefault() as ServicoJardinagem;
        }

        // Abre a tela para cadastrar novo serviço
        private async void OnNovoServicoClicked(object sender, EventArgs e)
        {
            ServicoParaEditar = null; // novo serviço, sem dados
            await Shell.Current.GoToAsync("formulario");
        }

        // Abre a tela para editar o serviço selecionado
        private async void OnEditarServicoClicked(object sender, EventArgs e)
        {

            // Verifica se um serviço foi selecionado
            if (_servicoSelecionado == null)
            {
                await DisplayAlert("Aviso", "Selecione um serviço para editar.", "OK");
                return;
            }

            // Clona para editar sem mexer direto no item da lista
            ServicoParaEditar = _servicoSelecionado.Clonar();
            
            // Navega para o formulário de edição
            await Shell.Current.GoToAsync("formulario");
        }

        // Exclui o serviço selecionado após confirmação
        private async void OnExcluirServicoClicked(object sender, EventArgs e)
        {
            // Verifica se um serviço foi selecionado
            if (_servicoSelecionado == null)
            {
                await DisplayAlert("Aviso", "Selecione um serviço para excluir.", "OK");
                return;
            }

            // Pergunta ao usuário se ele tem certeza que quer excluir
            bool confirmar = await DisplayAlert(
                "Confirmar",
                $"Deseja excluir o serviço de {_servicoSelecionado.Cliente}?",
                "Sim",
                "Não");

            // Se o usuário confirmar, remove o serviço do repositório
            if (confirmar)
            {
                ServicoRepository.Remover(_servicoSelecionado);
                // >>> ALTERAÇÃO: limpa a seleção após excluir
                ServicosCollectionView.SelectedItem = null;
            }
        }



        // Quando a página reaparece, atualiza a lista e limpa a seleção
        protected override void OnAppearing()
        {
            // Atualiza a lista para refletir quaisquer mudanças feitas na página de formulário
            base.OnAppearing();
            // Garante que a CollectionView continue ligada à coleção
            _servicoSelecionado = null;
            // Limpa a seleção para evitar confusão
            ServicosCollectionView.SelectedItem = null;
        }
    }
}