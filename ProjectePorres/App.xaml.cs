using System.Windows;
using ProjectePorres.Views.Windows;

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
                // LoginView no és visible i encara està carregada.
                if (!loginView.IsVisible && loginView.IsLoaded)
                {
                    mainWindow.Show();
                    loginView.Hide();
                }
            };

            mainWindow.IsVisibleChanged += (s, ev) =>
            {
                // MainWindow no és visible i encara està carregada.
                if (!mainWindow.IsVisible && mainWindow.IsLoaded)
                {
                    loginView.Show();
                    mainWindow.Hide();
                }
            };
        }
    }
}
