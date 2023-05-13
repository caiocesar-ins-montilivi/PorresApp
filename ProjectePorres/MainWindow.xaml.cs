using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using Ookii.Dialogs.Wpf;

namespace ProjectePorres
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Events
        private void ButtonExit_Click(object sender, RoutedEventArgs e) => ShowExitDialog();


        private void ItemHome_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void ItemPorres_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void ItemEquips_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void ItemRecomanar_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void ItemExit_MouseUp(object sender, MouseButtonEventArgs e) => ShowExitDialog();

        private void ItemSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        // Methods

        // Dialogs
        private void ShowExitDialog()
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

            TaskDialogButton button = dialog.ShowDialog(this);

            if (button == buttonTancarSessio)
            {
                Close();
                // Obrir dialog d'inici de sessió.
            }
            else if (button == buttonTancarApp)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
