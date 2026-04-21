namespace JardimSimplesApp.Views;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // >>> ALTERAÇÃO: tempo de exibição da splash
        await Task.Delay(3000);

        // >>> ALTERAÇÃO: abre o AppShell após a splash
        Application.Current!.Windows[0].Page = new AppShell();
    }
}