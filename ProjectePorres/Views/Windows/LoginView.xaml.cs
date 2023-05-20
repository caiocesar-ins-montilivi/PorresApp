using System.Windows;
using System.Windows.Controls;
using MaterialDesignExtensions.Controls;

namespace ProjectePorres.Views.Windows
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : MaterialWindow
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBoxContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
        }

        private void PasswordBoxRegistrarContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).RPassword = ((PasswordBox)sender).SecurePassword;
        }

        private void PasswordBoxRepetirRegistrarContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).RPassword2 = ((PasswordBox)sender).SecurePassword;
        }
    }
}
