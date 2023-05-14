using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // Camps
        private string _nomUsuari;
        private SecureString _password;
        private string _errorMessage;
        private int _indexTabActual;
        private bool _isViewVisible = true;
        private bool _mantenirSessio;

        private string r_nomUsuari;
        private string r_email;
        private SecureString r_password;
        private SecureString r_password2;

        // Propietats
        public string NomUsuari
        {
            get => _nomUsuari;
            set
            {
                _nomUsuari = value;
                OnPropertyChanged(nameof(NomUsuari));
            }
        }

        public SecureString SecurePassword
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(SecurePassword));
            }
        }

        public string RNomUsuari
        {
            get => r_nomUsuari;
            set
            {
                r_nomUsuari = value;
                OnPropertyChanged(nameof(RNomUsuari));
            }
        }
        public string REmail
        {
            get => r_email;
            set
            {
                r_email = value;
                OnPropertyChanged(nameof(REmail));
            }
        }
        public SecureString RPassword
        {
            get => r_password;
            set
            {
                r_password = value;
                OnPropertyChanged(nameof(RPassword));
            }
        }
        public SecureString RPassword2
        {
            get => r_password2;
            set
            {
                r_password2 = value;
                OnPropertyChanged(nameof(RPassword2));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set 
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool MantenirSessio
        {
            get => _mantenirSessio;
            set
            {
                _mantenirSessio = value;
                OnPropertyChanged(nameof(MantenirSessio));
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

        public int IndexTabActual
        {
            get { return _indexTabActual; }
            set
            {
                _indexTabActual = value;
                OnPropertyChanged(nameof(IndexTabActual));
            }
        }

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ChangeTabCommand { get; }
        public ICommand RecuperarContrasenyaComand { get; }
        public ICommand MostrarPasswordCommand { get; }
        public ICommand MantenirSessioCommand { get; }
        public ICommand SortirCommand { get; }

        // Constructor 
        public LoginViewModel()
        {
            LoginCommand = new CommandViewModel(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new CommandViewModel(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            ChangeTabCommand = new CommandViewModel(ExecuteChangeTab);
            RecuperarContrasenyaComand = new CommandViewModel(p => ExecuteRecuperarPassCommand("", ""));
            SortirCommand = new CommandViewModel(ExecuteSortirCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {
            // IsViewVisible = false;
            Trace.WriteLine($"{NomUsuari} {SecurePassword} {MantenirSessio}");
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool dadesValides;
            
            if (string.IsNullOrWhiteSpace(NomUsuari) || NomUsuari.Length < 3 || SecurePassword == null || SecurePassword.Length < 3)
                dadesValides = false;
            else
                dadesValides = true;
            
            return dadesValides;
        }

        private void ExecuteRegisterCommand(object obj)
        {
            Trace.WriteLine($"{RNomUsuari} {REmail}");
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            if (!string.IsNullOrWhiteSpace(RNomUsuari) && RNomUsuari.Length > 3
                && !string.IsNullOrWhiteSpace(REmail) && REmail.Length > 3
                && RPassword != null && RPassword2 != null
                && RPassword.Length > 3 && RPassword2.Length > 3)
                return true;
            return false;
        }

        private void ExecuteRecuperarPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
        
        private void ExecuteChangeTab(object obj)
        {
            if (IndexTabActual == 0) IndexTabActual = 1;
            else IndexTabActual = 0;
        }

        private void ExecuteSortirCommand(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
