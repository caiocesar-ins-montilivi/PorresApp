using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using ProjectePorres.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectePorres.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        // Camps
        private Swatch _primarySwatch;
        private Swatch _accentSwatch;
        private int _primaryIndex;
        private int _accentIndex;
        private bool _darkThemeEnabled;
        private readonly ConfigContext config;

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

        public int PrimaryIndex
        {
            get => _primaryIndex;
            set
            {
                _primaryIndex = value;
                OnPropertyChanged(nameof(PrimaryIndex));
            }
        }

        public int AccentIndex
        {
            get => _accentIndex;
            set
            {
                _accentIndex = value;
                OnPropertyChanged(nameof(AccentIndex));
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

            config = new("../../../settings.ini");

            // Agafa valors de l'arxiu de configuració
            string primaryColor = config.RetornarValor("AppTheme", "PrimaryColor");
            string secondaryColor = config.RetornarValor("AppTheme", "SecondaryColor");

            // Aplica valors de l'arxiu de configuració
            if (primaryColor != null && secondaryColor != null)
            {
                ModificarTema(theme => theme.SetPrimaryColor((Color)ColorConverter.ConvertFromString(primaryColor)));
                ModificarTema(theme => theme.SetSecondaryColor((Color)ColorConverter.ConvertFromString(secondaryColor)));
            }

            DarkThemeEnabled = Convert.ToBoolean(config.RetornarValor("AppTheme", "DarkMode"));
            PrimaryIndex = Convert.ToInt32(config.RetornarValor("AppTheme", "PrimaryIndex"));
            AccentIndex = Convert.ToInt32(config.RetornarValor("AppTheme", "AccentIndex"));        
        }

        private void ExecuteAplicarTema(object obj)  
        {
            if (PrimarySwatch != null)
            {
                ModificarTema(theme => theme.SetPrimaryColor(PrimarySwatch.ExemplarHue.Color));
                config.EscriureConfiguracio("AppTheme", "PrimaryColor", PrimarySwatch.ExemplarHue.Color.ToString());
                config.EscriureConfiguracio("AppTheme", "PrimaryIndex", PrimaryIndex.ToString());
            }
        } 

        private void ExecuteAplicarAccent(object obj)
        {
            if (AccentSwatch is { AccentExemplarHue: not null })
            {
                ModificarTema(theme => theme.SetSecondaryColor(AccentSwatch.AccentExemplarHue.Color));
                config.EscriureConfiguracio("AppTheme", "SecondaryColor", AccentSwatch.AccentExemplarHue.Color.ToString());
                config.EscriureConfiguracio("AppTheme", "AccentIndex", AccentIndex.ToString());
            }
        }

        private void ExecuteAplicarDarkTheme(object obj) => AplicarDarkTheme();

        private static void ModificarTema(Action<ITheme> action)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            action?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        private void AplicarDarkTheme()
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

            config.EscriureConfiguracio("AppTheme", "DarkMode", DarkThemeEnabled.ToString());
        }
    }
}
