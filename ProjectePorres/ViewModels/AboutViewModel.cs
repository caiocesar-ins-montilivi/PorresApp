using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class AboutViewModel : BaseViewModel
    {
        // Camps
        private bool _actualitzarProgressBarVisible;
        private string _repoUrl = "https://github.com/caiocesar-ins-montilivi/PorresApp";

        // Propietats
        public bool ActualitzarProgressBarVisible
        {
            get => _actualitzarProgressBarVisible;
            set
            {
                _actualitzarProgressBarVisible = value;
                OnPropertyChanged(nameof(ActualitzarProgressBarVisible));
            }
        }

        // Commands
        public ICommand ActualitzarCommand { get; }
        public ICommand AnarGithubCommand { get; }

        // Constructor
        public AboutViewModel()
        {
            ActualitzarCommand = new CommandViewModel(ExecuteActualitzarCommand);
            AnarGithubCommand = new CommandViewModel(ExecuteAnarGithubCommand);
        }

        private async void ExecuteActualitzarCommand(object obj)
        {

            ActualitzarProgressBarVisible = true;

            await Task.Delay(1000);

            ActualitzarProgressBarVisible = false;

            MessageBox.Show("Versió més recent instal·lada (v.1.0.0)", "Actualtizar | Cerca actualitzacions", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteAnarGithubCommand(object obj)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _repoUrl,
                UseShellExecute = true,
            });
        }
    }
}
