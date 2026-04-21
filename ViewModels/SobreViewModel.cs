namespace JardimSimplesApp.ViewModels;

public class SobreViewModel : BaseViewModel
{
    public string NomeAplicativo => "Jardim Simples App";

    public string Descricao =>
        "Aplicativo didático em .NET MAUI com arquitetura MVVM, criado para demonstrar uma lista, um formulário de cadastro e edição, seleção de itens e uma tela sobre.";

    public string Recursos =>
        "• Lista de serviços" +
        "• Seleção de item" +
        "• Cadastro e edição" +
        "• Exclusão" +
        "• Navegação entre telas" +
        "• Separação entre View, ViewModel e Model";
}
