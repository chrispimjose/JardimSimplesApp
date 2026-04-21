using System.Collections.ObjectModel;
using System.Windows.Input;
using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using JardimSimplesApp.Views;

namespace JardimSimplesApp.ViewModels;

// ViewModel para a página de listagem de serviços.
public class ListaServicosViewModel : BaseViewModel
{
    // Campo privado para armazenar o item selecionado.
    private ServicoJardinagem? _servicoSelecionado;

    // Comando para editar o item selecionado.
    public Command EditarCommand { get; }

    // A View enxerga esta coleção e exibe os itens na lista.
    public ObservableCollection<ServicoJardinagem> Servicos => ServicoRepository.Servicos;

    // Item selecionado na tela.
    public ServicoJardinagem? ServicoSelecionado
    {
        get => _servicoSelecionado;
        //set => SetProperty(ref _servicoSelecionado, value);
        set
        {
            _servicoSelecionado = value;
            OnPropertyChanged();

            // Atualiza comando automaticamente
            EditarCommand.ChangeCanExecute();
        }
    }

    // Construtor onde inicializamos o comando.
    public ListaServicosViewModel()
    {
        EditarCommand = new Command(
            async () => await Editar(),
            () => ServicoSelecionado != null // habilita só se tiver seleção
        );
    }

    // Método para editar o item selecionado, navegando para o formulário.
    private async Task Editar()
    {
        if (ServicoSelecionado == null)
        {
            await Shell.Current.DisplayAlert("Debug", "ServicoSelecionado é nulo!", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Debug", $"Editando: {ServicoSelecionado.Cliente}", "OK");

        ListaServicosPage.ServicoParaEditar = ServicoSelecionado.Clonar();
        await Shell.Current.GoToAsync("formulario");

    }


    // Comando para limpar a seleção, útil quando a página reaparece.
    public ICommand LimparSelecaoCommand { get; }

    // Método para limpar a seleção, garantindo que o item anterior não permaneça selecionado.
    private void LimparSelecao()
    {
        ServicoSelecionado = null;
    }

}
