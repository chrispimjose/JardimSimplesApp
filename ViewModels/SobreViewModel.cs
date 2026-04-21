namespace JardimSimplesApp.ViewModels;

public class SobreViewModel : BaseViewModel
{
    // Propriedades para exibir informações sobre o aplicativo
    public string NomeAplicativo => "JardimSimples App";

    // Descrição do aplicativo, incluindo o nome do professor responsável
    public string Descricao =>
        "Aplicativo didático em .NET MAUI com arquitetura MVVM, criado para demonstrar uma lista, um formulário de cadastro e edição, seleção de itens e uma tela sobre.\nElaborado: Professor José Padilha ";

    // Recursos e funcionalidades do aplicativo
    public string Recursos =>
        "• Lista de serviços" +
        "• Seleção de item" +
        "• Cadastro e edição" +
        "• Exclusão" +
        "• Navegação entre telas" +
        "• Separação entre View, ViewModel e Model";
}
