using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using System.Globalization;

namespace JardimSimplesApp.Views
{
    public partial class FormServicoPage : ContentPage
    {
        private ServicoJardinagem _servicoEmEdicao = new ServicoJardinagem();

        public FormServicoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();



            if (ListaServicosPage.ServicoParaEditar != null)
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
            else
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

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClienteEntry.Text) ||
                string.IsNullOrWhiteSpace(TipoServicoEntry.Text) ||
                string.IsNullOrWhiteSpace(EnderecoEntry.Text) ||
                string.IsNullOrWhiteSpace(ValorEntry.Text) ||
                StatusPicker.SelectedItem == null)
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
                return;
            }


            string textoValor = ValorEntry.Text.Replace(',', '.');
            if (!decimal.TryParse(textoValor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                await DisplayAlert("Erro", "Digite um valor válido.", "OK");
                return;
            }

            _servicoEmEdicao.Cliente = ClienteEntry.Text.Trim();
            _servicoEmEdicao.TipoServico = TipoServicoEntry.Text.Trim();
            _servicoEmEdicao.Endereco = EnderecoEntry.Text.Trim();
            _servicoEmEdicao.DataServico = DataServicoPicker.Date;
            _servicoEmEdicao.Valor = valor;
            _servicoEmEdicao.Status = StatusPicker.SelectedItem.ToString() ?? "Agendado";


            if (_servicoEmEdicao.Id > 0)
            {
                ServicoRepository.Atualizar(_servicoEmEdicao);
                await DisplayAlert("Sucesso", "Serviço cadastrado com sucesso.", "OK");
            }
            else
            {
                ServicoRepository.Adicionar(_servicoEmEdicao);
                await DisplayAlert("Sucesso", "Serviço cadastrado com sucesso.", "OK");
            }

            ListaServicosPage.ServicoParaEditar = null;
            await Shell.Current.GoToAsync("..");
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            if (_servicoEmEdicao.Id == 0) 
            { 
                await DisplayAlert("Atenção", "Esse serviço ainda não foi salvo.", "OK");
            return; 
            }

            bool confirmar = await DisplayAlert(
                "Confirmação",
                "Deseja realmente excluir este serviço?",
                "Sim", "Não");

            if (confirmar)
            {
                ServicoRepository.RemoverPorId(_servicoEmEdicao.Id);
                ListaServicosPage.ServicoParaEditar = null;
                await DisplayAlert("Sucesso", "Serviço excluído.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            ListaServicosPage.ServicoParaEditar = null;
            await Shell.Current.GoToAsync("..");
        }

    }
}
