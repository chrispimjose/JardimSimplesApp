using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JardimSimplesApp.ViewModels;

// Classe base para todos os ViewModels.
// Ela oferece notificação de mudança de propriedades para a interface.
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(propertyName);
        return true;
    }
}
