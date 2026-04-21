using System.Collections.ObjectModel;
using System.Windows.Input;
using JardimSimplesApp.Models;
using JardimSimplesApp.Services;
using JardimSimplesApp.Views;

namespace JardimSimplesApp.ViewModels;

public class ListaServicosViewModel : BaseViewModel
{
    private ServicoJardinagem? _servicoSelecionado;
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

    public ListaServicosViewModel()
    {
        EditarCommand = new Command(
            async () => await Editar(),
            () => ServicoSelecionado != null // habilita só se tiver seleção
        );
    }

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


    private void LimparSelecao()
    {
        ServicoSelecionado = null;
    }

}
