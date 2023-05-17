using System.Windows;
using ProjectePorres.ViewModels;
using ProjectePorres.Views;

namespace ProjectePorres
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            LoginView loginView = new();
            loginView.Show();

            MainWindow mainWindow = new();

            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (!loginView.IsVisible && loginView.IsLoaded && !LoginViewModel.Instance.IniciatSessio)
                {
                    mainWindow.Show();
                    loginView.Hide();
                }
            };

            mainWindow.IsVisibleChanged += (s, ev) =>
            {
                if (!mainWindow.IsVisible && mainWindow.IsLoaded)
                {
                    loginView.Show();
                    mainWindow.Hide();
                }
            };
        }
    }
}
