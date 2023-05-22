using MySql.Data.MySqlClient;
using ProjectePorres.Data;
using ProjectePorres.Model;
using System;
using System.Diagnostics;
using System.Security;
using System.Text.RegularExpressions;
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
        private bool _iniciatSessio;
        private bool _progressBarLoginVisible;
        private bool _progressBarRegistreVisible;

        private string r_nomUsuari;
        private string r_dni;
        private string r_nom;
        private string r_cognom;
        private string r_correu;
        private SecureString r_password;
        private SecureString r_password2;

         private readonly DatabaseContext databaseContext;

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

        public string Dni
        {
            get => r_dni;
            set
            {
                r_dni = value;
                OnPropertyChanged(nameof(Dni));
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

        public string RNom
        {
            get => r_nom;
            set
            {
                r_nom = value;
                OnPropertyChanged(nameof(RNom));
            }
        }

        public string RCognom
        {
            get => r_cognom;
            set
            {
                r_cognom = value;
                OnPropertyChanged(nameof(RCognom));
            }
        }

        public string RCorreu
        {
            get => r_correu;
            set
            {
                r_correu = value;
                OnPropertyChanged(nameof(RCorreu));
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

        public static LoginViewModel Instance { get { return new LoginViewModel(); } }

        public bool IniciatSessio
        {
            get { return _iniciatSessio; }
            set
            {
                _iniciatSessio = value;
                OnPropertyChanged(nameof(IniciatSessio));
            }
        }

        public bool ProgressBarLoginVisible
        {
            get { return _progressBarLoginVisible; }
            set
            {
                _progressBarLoginVisible = value;
                OnPropertyChanged(nameof(ProgressBarLoginVisible));
            }
        }

        public bool ProgressBarRegistreVisible
        {
            get { return _progressBarRegistreVisible; }
            set
            {
                _progressBarRegistreVisible = value;
                OnPropertyChanged(nameof(ProgressBarRegistreVisible));
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
            // Ens connectem a la base de dades.
            const string connectionString = "Server=localhost; Database=PorraGirona; Uid=root; Pwd=;";
            databaseContext = new DatabaseContext(connectionString);

            LoginCommand = new CommandViewModel(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new CommandViewModel(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            ChangeTabCommand = new CommandViewModel(ExecuteChangeTab);
            RecuperarContrasenyaComand = new CommandViewModel(p => ExecuteRecuperarPassCommand("", ""));
            SortirCommand = new CommandViewModel(ExecuteSortirCommand);
        }

        private async void ExecuteLoginCommand(object obj)
        {
            ProgressBarLoginVisible = true;
            await Task.Delay(100);
            bool connectat = await databaseContext.ComprovarConnexio();

            if (connectat)
            {
                // Convertim SecureString en string TODO: NO ÉS LA MILLOR OPCIÓ
                string password = new System.Net.NetworkCredential(string.Empty, SecurePassword).Password;
                UsuariModel usuari = await databaseContext.RetornarUsuariPerNom(NomUsuari);

                if (databaseContext.ValidarUsuari(NomUsuari, password))
                {
                    IniciatSessio = true;
                    IsViewVisible = false;

                    Usuari = usuari;
                    MainWindowViewModel.Instance.IsViewVisible = true;
                    ProgressBarLoginVisible = false;
                }
                else
                {
                    ProgressBarLoginVisible = false;
                    ErrorMessage = "Usuari o contrasenya incorrecte.";
                    MessageBox.Show(ErrorMessage, "Error | Autenticació", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ProgressBarLoginVisible = false;
                ErrorMessage = "No es pot accedir a la base de dades.";
                MessageBox.Show(ErrorMessage, "Error | Autenticació", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private async void ExecuteRegisterCommand(object obj)
        {
            ProgressBarRegistreVisible = true;
            await Task.Delay(300);
            bool connectat = await databaseContext.ComprovarConnexio();
            
            if (connectat)
            {
                string password = new System.Net.NetworkCredential(string.Empty, RPassword).Password;

                bool usuariRegistrat = await databaseContext.RegistrarUsuari(RNomUsuari, Dni, RNom, RCognom, RCorreu, password);

                if (usuariRegistrat)
                {
                    IndexTabActual = 0;
                    MessageBox.Show("Has sigut registrat correctament", "Registre correcte", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProgressBarRegistreVisible = false;
                }
                else
                {
                    ProgressBarRegistreVisible = false;
                    ErrorMessage = "No s'ha pogut registrar-te, revisa els camps.";
                    MessageBox.Show(ErrorMessage, "Error | Registre", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ProgressBarRegistreVisible = false;
                ErrorMessage = "No es pot accedir a la base de dades.";
                MessageBox.Show(ErrorMessage, "Error | Registre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            string password = new System.Net.NetworkCredential(string.Empty, RPassword).Password;
            string password2 = new System.Net.NetworkCredential(string.Empty, RPassword2).Password;

            if (!string.IsNullOrWhiteSpace(RNomUsuari) && RNomUsuari.Length > 3
                && !string.IsNullOrWhiteSpace(RCorreu) && RCorreu.Length > 3
                && !string.IsNullOrWhiteSpace(RNom) && RNom.Length > 3
                && !string.IsNullOrWhiteSpace(RCognom) && RCognom.Length > 3
                && ValidarCorreu() && ValidarDni()
                && password == password2
                && RPassword != null && RPassword2 != null
                && RPassword.Length > 3 && RPassword2.Length > 3)
                return true;

            return false;
        }
        
        private bool ValidarDni()
        {
            bool esValid = false;
            const string dniPattern = @"^\d{8}[A-HJ-NP-TV-Z]$";
            const string niePattern = @"^[XYZ]\d{7}[A-HJ-NP-TV-Z]$";

            // Verificar si es un DNI
            if (!string.IsNullOrWhiteSpace(Dni))
            {
                if (Regex.IsMatch(Dni, dniPattern))
                {
                    string dniDigits = Dni.Substring(0, Dni.Length - 1);
                    char dniLetter = char.ToUpper(Dni[Dni.Length - 1]);

                    // Verificar que la lletra sigui vàlida
                    char calculatedLetter = "TRWAGMYFPDXBNJZSQVHLCKE"[int.Parse(dniDigits) % 23];
                    if (calculatedLetter != dniLetter) esValid = false;
                    else esValid = true;
                }

                // Verificar si és un NIE
                else if (Regex.IsMatch(Dni, niePattern))
                {
                    // Verificar que el número del NIE sigui vàlid
                    if (!"XYZ".Contains(char.ToUpper(Dni[0]))) esValid = false;
                    else esValid = true;
                }

                // No és cap
                else esValid = false;
            }
            return esValid;
        }

        private bool ValidarCorreu()
        {
            const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(RCorreu, pattern);
        }

        private void ExecuteRecuperarPassCommand(string username, string email)
        {
            MessageBox.Show("No implementat", "En desenvolupament", MessageBoxButton.OK, MessageBoxImage.Information);
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
