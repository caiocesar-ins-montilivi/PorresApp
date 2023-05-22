using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        // Camps
        private Swatch _primarySwatch;
        private Swatch _accentSwatch;
        private bool _darkThemeEnabled;

        // Propietats
        public IEnumerable<Swatch> Mostres { get; }

        public Swatch PrimarySwatch 
        {
            get => _primarySwatch;
            set
            {
                _primarySwatch = value;
                OnPropertyChanged(nameof(PrimarySwatch));
            }
        }

        public Swatch AccentSwatch
        {
            get => _accentSwatch;
            set
            {
                _accentSwatch = value;
                OnPropertyChanged(nameof(AccentSwatch));
            }
        }

        public bool DarkThemeEnabled
        {
            get => _darkThemeEnabled;
            set
            {
                _darkThemeEnabled = value;
                OnPropertyChanged(nameof(DarkThemeEnabled));
                AplicarDarkThemeCommand.Execute(this);
            }
        }

        // Command
        public ICommand AplicarTemaCommand { get; }
        public ICommand AplicarAccentCommand { get; }
        public ICommand AplicarDarkThemeCommand { get; }

        public SettingsViewModel()
        {
            Mostres = new SwatchesProvider().Swatches;
            AplicarTemaCommand = new CommandViewModel(ExecuteAplicarTema);
            AplicarAccentCommand = new CommandViewModel(ExecuteAplicarAccent);
            AplicarDarkThemeCommand = new CommandViewModel(ExecuteAplicarDarkTheme);
        }

        private void ExecuteAplicarTema(object obj)  
        {
            if (PrimarySwatch != null)
            {
                ModificarTema(theme => theme.SetPrimaryColor(PrimarySwatch.ExemplarHue.Color));
            }
        } 

        private void ExecuteAplicarAccent(object obj)
        {
            if (AccentSwatch is { AccentExemplarHue: not null })
            {
                ModificarTema(theme => theme.SetSecondaryColor(AccentSwatch.AccentExemplarHue.Color));
            }
        }

        private void ExecuteAplicarDarkTheme(object obj)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            if (DarkThemeEnabled)
            {
                theme.SetBaseTheme(Theme.Dark);
                paletteHelper.SetTheme(theme);
            }
            else
            {
                theme.SetBaseTheme(Theme.Light);
                paletteHelper.SetTheme(theme);
            }
        }

        private void ModificarTema(Action<ITheme> action)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            action?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}
