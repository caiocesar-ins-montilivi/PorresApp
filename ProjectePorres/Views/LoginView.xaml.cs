using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MaterialDesignExtensions.Controls;

namespace ProjectePorres.Views
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

        private void PasswordBoxRepetirRegistrarContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).RPassword2 = ((PasswordBox)sender).SecurePassword;
        }

        private void PasswordBoxRegistrarContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).RPassword = ((PasswordBox)sender).SecurePassword;
        }
    }
}
