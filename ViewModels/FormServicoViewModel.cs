using JardimSimplesApp.Models;
using JardimSimplesApp.Services;

namespace JardimSimplesApp.ViewModels;

public class FormServicoViewModel : BaseViewModel
{
    private int _id;
    private string _cliente = string.Empty;
    private string _tipoServico = string.Empty;
    private string _endereco = string.Empty;
    private DateTime _dataServico = DateTime.Today;
    private string _valorTexto = string.Empty;
    private string _status = "Agendado";
    private bool _estaEditando;

    // Lista fixa de opções para o Picker de status.
    public List<string> StatusDisponiveis { get; } = new() { "Agendado", "Em andamento", "Concluído" };

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Cliente
    {
        get => _cliente;
        set => SetProperty(ref _cliente, value);
    }

    public string TipoServico
    {
        get => _tipoServico;
        set => SetProperty(ref _tipoServico, value);
    }

    public string Endereco
    {
        get => _endereco;
        set => SetProperty(ref _endereco, value);
    }

    public DateTime DataServico
    {
        get => _dataServico;
        set => SetProperty(ref _dataServico, value);
    }

    // Mantivemos o valor como texto para simplificar a digitação no Entry.
    // Depois, durante o salvamento, fazemos a conversão para decimal.
    public string ValorTexto
    {
        get => _valorTexto;
        set => SetProperty(ref _valorTexto, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public bool EstaEditando
    {
        get => _estaEditando;
        set => SetProperty(ref _estaEditando, value);
    }

    public string TituloFormulario => EstaEditando ? "Editar Serviço" : "Novo Serviço";

    public void CarregarParaEdicao(ServicoJardinagem servico)
    {
        Id = servico.Id;
        Cliente = servico.Cliente;
        TipoServico = servico.TipoServico;
        Endereco = servico.Endereco;
        DataServico = servico.DataServico;
        ValorTexto = servico.Valor.ToString("F2");
        Status = servico.Status;
        EstaEditando = true;
        OnPropertyChanged(nameof(TituloFormulario));
    }

    public void PrepararNovoCadastro()
    {
        Id = 0;
        Cliente = string.Empty;
        TipoServico = string.Empty;
        Endereco = string.Empty;
        DataServico = DateTime.Today;
        ValorTexto = string.Empty;
        Status = "Agendado";
        EstaEditando = false;
        OnPropertyChanged(nameof(TituloFormulario));
    }

    public bool ValidarCampos(out string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(Cliente) ||
            string.IsNullOrWhiteSpace(TipoServico) ||
            string.IsNullOrWhiteSpace(Endereco) ||
            string.IsNullOrWhiteSpace(ValorTexto) ||
            string.IsNullOrWhiteSpace(Status))
        {
            mensagemErro = "Preencha todos os campos.";
            return false;
        }

        if (!decimal.TryParse(ValorTexto, out _))
        {
            mensagemErro = "Digite um valor numérico válido.";
            return false;
        }

        mensagemErro = string.Empty;
        return true;
    }

    public void Salvar()
    {
        decimal valor = decimal.Parse(ValorTexto);

        if (EstaEditando)
        {
            var servicoAtualizado = new ServicoJardinagem
            {
                Id = Id,
                Cliente = Cliente,
                TipoServico = TipoServico,
                Endereco = Endereco,
                DataServico = DataServico,
                Valor = valor,
                Status = Status
            };

            ServicoRepository.Atualizar(servicoAtualizado);
        }
        else
        {
            var novoServico = new ServicoJardinagem
            {
                Cliente = Cliente,
                TipoServico = TipoServico,
                Endereco = Endereco,
                DataServico = DataServico,
                Valor = valor,
                Status = Status
            };

            ServicoRepository.Adicionar(novoServico);
        }
    }

    public void Excluir()
    {
        var servico = ServicoRepository.Servicos.FirstOrDefault(s => s.Id == Id);

        if (servico is not null)
            ServicoRepository.Remover(servico);
    }
}
