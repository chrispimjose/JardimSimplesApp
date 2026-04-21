namespace JardimSimplesApp.Views;

public partial class SairPage : ContentPage
{
	public SairPage()
	{
		InitializeComponent();
	}

    private void OnSairClicked(object sender, EventArgs e)
    {
#if ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif WINDOWS
        Application.Current.Quit();
#endif
    }



}