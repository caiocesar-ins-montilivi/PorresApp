using ProjectePorres.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<PartitModel> _coleccioPartits;
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

            EquipModel barca = new("Barça", "Barcelona", "Spotify Camp Nou", "../../../Assets/barca.png", "Primera divisió");
            EquipModel girona = new("Girona", "Girona", "Camp Montilivi", "../../../Assets/girona.png", "Primera divisió");

            ColeccioPartits = new ObservableCollection<PartitModel>()
            {
                new PartitModel(barca, girona, new DateTime(2023, 05, 22), "2023", 2, 1, false),
                new PartitModel(girona, barca, new DateTime(2023, 05, 22), "2023", 3, 1, false),
                new PartitModel(girona, barca, new DateTime(2023, 05, 22), "2023", 1, 3, false),
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
