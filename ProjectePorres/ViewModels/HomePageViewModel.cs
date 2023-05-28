using ProjectePorres.Model;
using ProjectePorres.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<PartitModel> _coleccioPartits;
        private ObservableCollection<JugadorModel> _coleccioJugadors;
        private int _golsPartitSeleccionatLocal;
        private int _golsPartitSeleccionatVisitant;

        // Propietats
        public ObservableCollection<PartitModel> ColeccioPartits
        {
            get => _coleccioPartits;
            set
            {
                _coleccioPartits = value;
                OnPropertyChanged(nameof(ColeccioPartits));
            }
        }

        public ObservableCollection<JugadorModel> ColeccioJugadors
        {
            get => _coleccioJugadors;
            set
            {
                _coleccioJugadors = value;
                OnPropertyChanged(nameof(ColeccioJugadors));
            }
        }

        public int GolsPartitSeleccionatLocal
        {
            get => _golsPartitSeleccionatLocal;
            set
            {
                _golsPartitSeleccionatLocal = value;
                OnPropertyChanged(nameof(GolsPartitSeleccionatLocal));
            }
        }

        public int GolsPartitSeleccionatVisitant
        {
            get => _golsPartitSeleccionatVisitant;
            set
            {
                _golsPartitSeleccionatVisitant = value;
                OnPropertyChanged(nameof(GolsPartitSeleccionatVisitant));
            }
        }

        // Commands
        public ICommand PartitSeleccionatCommand { get; }

        // Constructor
        public HomePageViewModel()
        {
            PartitSeleccionatCommand = new CommandViewModel(ExecutePartitSeleccionatCommand);

            ColeccioJugadors = new ObservableCollection<JugadorModel>()
            {
                new JugadorModel(1, "Manolo", "2023-2024", 20),
                new JugadorModel(2, "Javier", "2023-2024", 4),
                new JugadorModel(3, "Juan", "2023-2024", 10),
            };

            EquipModel barca = new(0, "Barça", "Barcelona", "Spotify Camp Nou", "../../../Assets/barca.png", "Primera divisió");
            EquipModel girona = new(1, "Girona", "Girona", "Camp Montilivi", "../../../Assets/girona.png", "Primera divisió");

            ColeccioPartits = new ObservableCollection<PartitModel>()
            {
                new PartitModel(1, barca, girona, new DateTime(2023, 05, 22), "2023-2024", 2, 1, false),
                new PartitModel(2, girona, barca, new DateTime(2023, 05, 22), "2023-2024", 3, 1, false),
                new PartitModel(3, girona, barca, new DateTime(2023, 05, 22), "2023-2024", 1, 3, false),
            };
        }

        private void ExecutePartitSeleccionatCommand(object obj)
        {

            if (obj is PartitModel selectedPartit)
            {
                int indexSeleccionat = ColeccioPartits.IndexOf(selectedPartit);
                GolsPartitSeleccionatLocal = ColeccioPartits[indexSeleccionat].GolsLocal;
                GolsPartitSeleccionatVisitant = ColeccioPartits[indexSeleccionat].GolsVisitant;
            }
        }
    }
}
