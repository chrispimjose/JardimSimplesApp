using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using System.Globalization;

namespace JardimSimplesApp.Views
{
    public partial class FormServicoPage : ContentPage
    {
        // Variável para armazenar o serviço em edição
        private ServicoJardinagem _servicoEmEdicao = new ServicoJardinagem();

        // Propriedade para acessar o serviço em edição
        public FormServicoPage()
        {
            InitializeComponent();
        }
        // Sobrescreve o método OnAppearing para carregar os dados do serviço a ser editado
        // O OnAppearing é chamado toda vez que a página aparece, garantindo que os dados estejam atualizados
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ListaServicosPage.ServicoParaEditar != null) // Verifica se há um serviço para editar
            {
                // Modo edição: preenche os campos
                _servicoEmEdicao = ListaServicosPage.ServicoParaEditar.Clonar();

                ClienteEntry.Text = _servicoEmEdicao.Cliente;
                TipoServicoEntry.Text = _servicoEmEdicao.TipoServico;
                EnderecoEntry.Text = _servicoEmEdicao.Endereco;
                DataServicoPicker.Date = _servicoEmEdicao.DataServico;
                ValorEntry.Text = _servicoEmEdicao.Valor.ToString("F2", CultureInfo.InvariantCulture);
                StatusPicker.SelectedItem = _servicoEmEdicao.Status;
            }
            else // Modo criação: limpa os campos
            {
                // Modo edição: preenche os campos
                _servicoEmEdicao = new ServicoJardinagem();

                ClienteEntry.Text = string.Empty;
                TipoServicoEntry.Text = string.Empty;
                EnderecoEntry.Text = string.Empty;
                DataServicoPicker.Date = DateTime.Today;
                ValorEntry.Text = string.Empty;
                StatusPicker.SelectedIndex = 0;
            }
        }

        // Método para salvar o serviço, seja criando um novo ou atualizando um existente
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            // Validação dos campos obrigatórios
            if (string.IsNullOrWhiteSpace(ClienteEntry.Text) ||
                string.IsNullOrWhiteSpace(TipoServicoEntry.Text) ||
                string.IsNullOrWhiteSpace(EnderecoEntry.Text) ||
                string.IsNullOrWhiteSpace(ValorEntry.Text) ||
                StatusPicker.SelectedItem == null)
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
                return;
            }

            // Substitui vírgula por ponto para garantir que o decimal seja interpretado corretamente
            string textoValor = ValorEntry.Text.Replace(',', '.');

            // Tenta converter o valor para decimal usando a cultura invariante
            if (!decimal.TryParse(textoValor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                await DisplayAlert("Erro", "Digite um valor válido.", "OK");
                return;
            }

            // Atualiza os dados do serviço em edição com os valores dos campos
            _servicoEmEdicao.Cliente = ClienteEntry.Text.Trim();
            _servicoEmEdicao.TipoServico = TipoServicoEntry.Text.Trim();
            _servicoEmEdicao.Endereco = EnderecoEntry.Text.Trim();
            _servicoEmEdicao.DataServico = DataServicoPicker.Date;
            _servicoEmEdicao.Valor = valor;
            _servicoEmEdicao.Status = StatusPicker.SelectedItem.ToString() ?? "Agendado";

            // Verifica se é um serviço existente (edição) ou um novo serviço (criação)
            if (_servicoEmEdicao.Id > 0) // Edição
            {
                ServicoRepository.Atualizar(_servicoEmEdicao);
                await DisplayAlert("Sucesso", "Serviço atualizado com sucesso.", "OK");
            }
            else // Criação
            {
                ServicoRepository.Adicionar(_servicoEmEdicao);
                await DisplayAlert("Sucesso", "Serviço cadastrado com sucesso.", "OK");
            }

            // Limpa a referência ao serviço em edição para evitar confusão na próxima vez que a página for aberta
            ListaServicosPage.ServicoParaEditar = null;
            // Navega de volta para a página anterior (lista de serviços)
            await Shell.Current.GoToAsync("..");
        }

        // Método para excluir o serviço, disponível apenas no modo edição
        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (_servicoEmEdicao.Id == 0) // Verifica se o serviço ainda não foi salvo (não tem ID)
            { 
                await DisplayAlert("Atenção", "Esse serviço ainda não foi salvo.", "OK");
            return; 
            }

            // Exibe uma confirmação antes de excluir o serviço
            bool confirmar = await DisplayAlert(
                "Confirmação",
                "Deseja realmente excluir este serviço?",
                "Sim", "Não");

            if (confirmar) 
            {
                // Se o usuário confirmar a exclusão, remove o serviço
                // do repositório e navega de volta para a lista de serviços
                ServicoRepository.RemoverPorId(_servicoEmEdicao.Id);
                ListaServicosPage.ServicoParaEditar = null;
                await DisplayAlert("Sucesso", "Serviço excluído.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        // Método para cancelar a operação de criação ou edição,
        // limpando a referência ao serviço em edição e navegando de volta para a lista de serviços
        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            ListaServicosPage.ServicoParaEditar = null;
            await Shell.Current.GoToAsync("..");
        }

    }
}
