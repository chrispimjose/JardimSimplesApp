using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JardimSimplesApp.ViewModels;

// Classe base para todos os ViewModels.
// Ela oferece notificação de mudança de propriedades para a interface.
public class BaseViewModel : INotifyPropertyChanged
{
    // O INotifyPropertyChanged define o evento PropertyChanged,
    // que é acionado quando uma propriedade muda.
    // O método OnPropertyChanged é chamado para notificar os assinantes sobre a mudança de propriedade.
    public event PropertyChangedEventHandler? PropertyChanged;

    // O método OnPropertyChanged é protegido, permitindo
    // que as classes derivadas o chamem para notificar sobre mudanças de propriedade.
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // O método SetProperty é um método auxiliar para definir o valor de uma propriedade
    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(propertyName);
        return true;
    }
}
