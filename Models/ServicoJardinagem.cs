namespace JardimSimplesApp.Models;

public class ServicoJardinagem
{
    // Identificador único do serviço.
    public int Id { get; set; }

    // Nome do cliente.
    public string Cliente { get; set; } = string.Empty;

    // Tipo do serviço: corte, poda, limpeza etc.
    public string TipoServico { get; set; } = string.Empty;

    // Endereço de execução do serviço.
    public string Endereco { get; set; } = string.Empty;

    // Data agendada.
    public DateTime DataServico { get; set; } = DateTime.Today;

    // Valor do serviço.
    public decimal Valor { get; set; }

    // Situação atual do serviço.
    public string Status { get; set; } = "Agendado";

    public ServicoJardinagem Clonar()
    {
        return new ServicoJardinagem
        {
            Id = Id,
            Cliente = Cliente,
            TipoServico = TipoServico,
            Endereco = Endereco,
            DataServico = DataServico,
            Valor = Valor,
            Status = Status
        };
    }

}