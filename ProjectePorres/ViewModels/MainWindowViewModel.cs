﻿using Ookii.Dialogs.Wpf;
using ProjectePorres.Model;
using ProjectePorres.Views;
using ProjectePorres.Views.Pages;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool _tancantSessio;
        private bool _isViewVisible;

        // Pàgines
        private object? _vistaActual;
        private HomePage? homePage;
        private PorresPage? porresPage;
        private EquipsPage? equipsPage;
        private RecomanarPage? recomanarPage;
        private SettingsPage? settingsPage;

        // Propietats
        public bool TancantSessio
        {
            get => _tancantSessio;
            set
            {
                _tancantSessio = value;
                OnPropertyChanged(nameof(TancantSessio));
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

        public object? VistaActual
        {
            get => _vistaActual;
            set
            {
                _vistaActual = value;
                OnPropertyChanged(nameof(VistaActual));
            }
        }

        public static MainWindowViewModel Instance { get { return new MainWindowViewModel(); } }

        // Commands
        public ICommand CarregaHomeViewCommand { get; }
        public ICommand CarregaPorresViewCommand { get; }
        public ICommand CarregaEquipsViewCommand { get; }
        public ICommand CarregaRecomanarViewCommand { get; }
        public ICommand CarregaSettingsViewCommand { get; }

        public ICommand SortirCommand { get; }

        // Constructor
        public MainWindowViewModel()
        {
            CarregaHomeViewCommand = new CommandViewModel(ExecuteCarregaHome);
            CarregaPorresViewCommand = new CommandViewModel(ExecuteCarregaPorres);
            CarregaEquipsViewCommand = new CommandViewModel(ExecuteCarregaEquips);
            CarregaRecomanarViewCommand = new CommandViewModel(ExecuteCarregaRecomanar);
            CarregaSettingsViewCommand = new CommandViewModel(ExecuteCarregaSettings);
            SortirCommand = new CommandViewModel(ExecuteSortir);
        }

        private void ExecuteCarregaHome(object obj)
        {
            if (VistaActual != homePage || VistaActual is null)
            {

                if (Usuari != null) Trace.WriteLine(Usuari.ToString());
                else Trace.WriteLine("Na");


                homePage ??= new HomePage();
                VistaActual = homePage;
            }
        }

        private void ExecuteCarregaPorres(object obj)
        {
            if (VistaActual != porresPage || VistaActual is null)
            {
                porresPage ??= new PorresPage();
                VistaActual = porresPage;
            }
        }

        private void ExecuteCarregaEquips(object obj)
        {
            if (VistaActual != equipsPage || VistaActual is null)
            {
                equipsPage ??= new EquipsPage();
                VistaActual = equipsPage;
            }
        }

        private void ExecuteCarregaRecomanar(object obj)
        {
            if (VistaActual != recomanarPage || VistaActual is null)
            {
                recomanarPage ??= new RecomanarPage();
                VistaActual = recomanarPage;
            }
        }

        private void ExecuteCarregaSettings(object obj)
        {
            if (VistaActual != settingsPage || VistaActual is null)
            {
                settingsPage ??= new SettingsPage();
                VistaActual = settingsPage;
            }
        }

        // Utilitza un Ooki dialog
        private void ExecuteSortir(object obj)
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

            TaskDialogButton button = dialog.ShowDialog();

            if (button == buttonTancarSessio)
            {
                // Ocultar MainWindow
                TancantSessio = true;
                IsViewVisible = false;

                LoginViewModel.Instance.IniciatSessio = false;
                LoginViewModel.Instance.IsViewVisible = true;
            }
            else if (button == buttonTancarApp)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
