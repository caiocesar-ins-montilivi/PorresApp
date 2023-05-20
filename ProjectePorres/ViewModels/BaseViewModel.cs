using System.ComponentModel;
using ProjectePorres.Model;

namespace ProjectePorres.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public static UsuariModel? Usuari { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
