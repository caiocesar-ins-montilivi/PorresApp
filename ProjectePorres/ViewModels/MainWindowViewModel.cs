using Ookii.Dialogs.Wpf;
using ProjectePorres.Model;
using ProjectePorres.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private UsuariModel _usuari;
        private bool _tancantSessio;
        private bool _isViewVisible = false;
        private string _textBenvinguda;

        // Propietats
        public UsuariModel Usuari
        {
            get => _usuari;
            set
            {
                _usuari = value;
                OnPropertyChanged(nameof(Usuari));
            }
        }

        public bool TancantSessio
        {
            get => _tancantSessio;
            set
            {
                _tancantSessio = value;
                OnPropertyChanged(nameof(TancantSessio));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public string TextBenvinguda
        {
            get => _textBenvinguda;
            set
            {
                _textBenvinguda = value;
                OnPropertyChanged(nameof(TextBenvinguda));
            }
        }

        public static MainWindowViewModel Instance { get { return new(); } }

        // Commands
        public ICommand SortirCommand { get; }

        // Constructor
        public MainWindowViewModel()
        {
            SortirCommand = new CommandViewModel(ExecuteSortir);
            TextBenvinguda = $"Benvingut {Usuari}";
        }

        // Ooki dialog
        private void ExecuteSortir(object obj)
        {
            using TaskDialog dialog = new();
            dialog.WindowTitle = "Confirmació";
            dialog.Content = "Vols sortir de l'aplicació o només tancar sessió";
            dialog.MainIcon = TaskDialogIcon.Information;

            TaskDialogButton buttonTancarSessio = new("Tancar sessió");
            TaskDialogButton buttonTancarApp = new("Tancar app");
            TaskDialogButton cancelButton = new(ButtonType.Cancel);

            dialog.Buttons.Add(buttonTancarSessio);
            dialog.Buttons.Add(buttonTancarApp);
            dialog.Buttons.Add(cancelButton);

            TaskDialogButton button = dialog.ShowDialog();

            if (button == buttonTancarSessio)
            {
                // Ocultar MainWindow
                TancantSessio = true;
                IsViewVisible = false;

                LoginViewModel.Instance.IniciatSessio = false;
                LoginViewModel.Instance.IsViewVisible = true;
            }
            else if (button == buttonTancarApp)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
