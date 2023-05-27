using System.Windows;
using ProjectePorres.Data;
using ProjectePorres.ViewModels;
using ProjectePorres.Views.Windows;

namespace ProjectePorres
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Crear instàncies dls ViewModels.
            const string connectionString = "Server=localhost; Database=PorraGirona; Uid=root; Pwd=;";
            var databaseContext = new DatabaseContext(connectionString);
            var loginViewModel = new LoginViewModel();
            var mainWindowViewModel = new MainWindowViewModel();

            // Crear instàncies de les vistes i establir DataContext.
            var loginView = new LoginView { DataContext = loginViewModel };
            var mainWindow = new MainWindow { DataContext = mainWindowViewModel };

            // Mostra la finestra d'inci de sessió.
            bool connexio = await databaseContext.ComprovarConnexio();
            bool sessioActiva = await databaseContext.ComprovarSessionsActives();
            if (connexio)
            {
                if (sessioActiva)
                {
                    mainWindow.Show();
                }
                else
                {
                    loginView.Show();
                }
            }

            // Gestiona canvis en el ViewModel d'inici de sessió.
            loginViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(LoginViewModel.IniciatSessio))
                {
                    loginView.Hide();
                    mainWindow.Show();
                }
            };

            // Gestiona canvis en el ViewModel de la finestra principal.
            mainWindowViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MainWindowViewModel.TancantSessio))
                {
                    mainWindow.Hide();
                    loginView.Show();
                }
            };
        }
    }
}
