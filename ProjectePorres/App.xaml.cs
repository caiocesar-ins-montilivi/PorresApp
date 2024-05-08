using System;
using System.Windows;
using ProjectePorres.Data;
using ProjectePorres.Model;
using ProjectePorres.ViewModels;
using ProjectePorres.Views.Windows;

namespace ProjectePorres
{
    public partial class App
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Instància de configuració per treballar amb el fitxer de configuració.
            ConfigContext config = new("../../../settings.ini");

            // ConnectionString per fer alguna consulta a la base de dades.
            const string connectionString = "Server=localhost; Database=PorraGirona; Uid=root; Pwd=;";
            var databaseContext = new DatabaseContext(connectionString);

            // Crear instàncies dels ViewModels.
            var loginViewModel = new LoginViewModel();
            var mainWindowViewModel = new MainWindowViewModel();

            // Crear instàncies de les vistes i establir DataContext.
            var loginView = new LoginView { DataContext = loginViewModel };
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };

            bool connexio = await databaseContext.ComprovarConnexio();

            int userSessionId = Convert.ToInt32(config.RetornarValor("UserSessionId", "Id"));

            if (connexio)
            {
                if (userSessionId != 0)
                {
                    MostrarMainWindow();
                    ActualitzarUsuari();
                }
                else
                {
                    MostrarLoginView();
                    ActualitzarUsuari();
                }
            }
            else MostrarLoginView();

            // Gestiona canvis en el ViewModel d'inici de sessió.
            loginViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(LoginViewModel.IniciatSessio))
                {
                    MostrarMainWindow();
                    ActualitzarUsuari();
                }
            };

            // Gestiona canvis en el ViewModel de la finestra principal.
            mainWindowViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MainWindowViewModel.TancantSessio))
                {
                    MostrarLoginView();
                    ActualitzarUsuari();
                }
            };

            void MostrarMainWindow()
            {
                mainWindow.Show();
                loginView.Hide();
            }

            void MostrarLoginView()
            {
                mainWindow.Hide();
                loginView.Show();
            }

            async void ActualitzarUsuari()
            {
                int currentSessionId = Convert.ToInt32(config.RetornarValor("UserSessionId", "CurrentId"));
                UsuariModel usuari = await databaseContext.RetornarUsuariPerId(currentSessionId);
                mainWindowViewModel.Usuari = usuari;
            }
        }
    }
}
