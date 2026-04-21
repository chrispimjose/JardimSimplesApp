namespace JardimSimplesApp.Views;

public partial class SairPage : ContentPage
{
    // Construtor
    public SairPage()
	{
		InitializeComponent();
	}

    // Evento de clique para o botão "Sair"
    private void OnSairClicked(object sender, EventArgs e)
    {
        // Lógica para sair do aplicativo para Android e Windows
#if ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif WINDOWS
        Application.Current.Quit();
#endif
    }



}