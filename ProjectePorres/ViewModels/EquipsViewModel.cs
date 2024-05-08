using Org.BouncyCastle.Asn1.BC;
using ProjectePorres.Data;
using ProjectePorres.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class EquipsViewModel : BaseViewModel
    {
        // Camps
        private DatabaseContext databaseContext;
        private ObservableCollection<EquipModel>? _equips;

        private string? _nom;
        private string? _foto;
        private string? _ciutat;
        private string? _camp;
        private string? _categoria;

        private int? _modificarId;
        private string? _modificarNom;
        private string? _modificarFoto;
        private string? _modificarCiutat;
        private string? _modificarCamp;
        private string? _modificarCategoria;

        // Propietats
        public ObservableCollection<EquipModel>? Equips
        {
            get => _equips;
            set
            {
                _equips = value;
                OnPropertyChanged(nameof(Equips));
            }
        }

        public string? Nom 
        { 
            get => _nom; 
            set
            {
                _nom = value;
                OnPropertyChanged(nameof(Nom));
            } 
        }

        public string? Foto
        {
            get => _foto;
            set
            {
                _foto = value;
                OnPropertyChanged(nameof(Foto));
            }
        }

        public string? Ciutat 
        { 
            get => _ciutat;
            set
            {
                _ciutat = value;
                OnPropertyChanged(nameof(Ciutat));
            }
        }

        public string? Camp 
        { 
            get => _camp;
            set
            {
                _camp = value;
                OnPropertyChanged(nameof(Camp));
            }
        }

        public string? Categoria 
        { 
            get => _categoria;
            set
            {
                _categoria = value;
                OnPropertyChanged(nameof(Categoria));
            }
        }

        public int? ModificarId 
        { 
            get => _modificarId;
            set
            {
                _modificarId = value;
                OnPropertyChanged(nameof(ModificarId));
            }
        }

        public string? ModificarNom 
        { 
            get => _modificarNom;
            set
            {
                _modificarNom = value;
                OnPropertyChanged(nameof(ModificarNom));
            }
        }

        public string? ModificarFoto
        {
            get => _modificarFoto;
            set
            {
                _modificarFoto = value;
                OnPropertyChanged(nameof(ModificarFoto));
            }
        }

        public string? ModificarCiutat 
        { 
            get => _modificarCiutat;
            set
            {
                _modificarCiutat = value;
                OnPropertyChanged(nameof(ModificarCiutat));
            }
        }

        public string? ModificarCamp 
        { 
            get => _modificarCamp;
            set
            {
                _nom = value;
                OnPropertyChanged(nameof(ModificarCamp));
            }
        }

        public string? ModificarCategoria 
        { 
            get => _modificarCategoria;
            set
            {
                _modificarCategoria = value;
                OnPropertyChanged(nameof(ModificarCategoria));
            }
        }

        // Commands
        public ICommand SeleccionarFotoCommand { get; }
        public ICommand RegistrarEquipCommand { get; }
        public ICommand ModificarEquipCommand { get; }
        public ICommand LlistarEquipCommand { get; }

        // Constructor
        public EquipsViewModel()
        {
            string connectionString = "Server=localhost; Database=PorraGirona; Uid=root; Pwd=;";
            databaseContext = new DatabaseContext(connectionString);

            ActualitzarEquips();

            SeleccionarFotoCommand = new CommandViewModel(ExecuteSeleccionarFotoCommand);
            RegistrarEquipCommand = new CommandViewModel(ExecuteRegistrarEquipCommand, CanExecuteRegistrarEquipCommand);
            ModificarEquipCommand = new CommandViewModel(ExecuteModificarEquipCommand, CanExecuteModificarEquipCommand);
            LlistarEquipCommand = new CommandViewModel(ExecuteLListarEquipCommand);
        }

        private void ExecuteSeleccionarFotoCommand(object obj)
        {
            Foto = "";
        }

        private async void ExecuteRegistrarEquipCommand(object obj)
        {
            bool correcte = await databaseContext.RegistrarEquip(Nom!, Ciutat!, Camp!, Foto!, Categoria!);
            if (correcte)
            {
                MessageBox.Show("S'ha enregistrat l'equip correctament", "Registre correcte", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show("No s'ha pogut enregistrar l'equip, revisa els camps", "Registre incorrecte", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool CanExecuteRegistrarEquipCommand(object obj)
        {
            bool esValid = false;
            if (!string.IsNullOrWhiteSpace(Nom) && 
                !string.IsNullOrWhiteSpace(Ciutat) &&
                !string.IsNullOrWhiteSpace(Camp) &&
                !string.IsNullOrWhiteSpace(Foto) &&
                !string.IsNullOrWhiteSpace(Categoria))
                esValid = true;
            return esValid;
        }

        private void ExecuteModificarEquipCommand(object obj)
        {

        }

        private bool CanExecuteModificarEquipCommand(object obj)
        {
            bool esValid = false;
            if (!string.IsNullOrWhiteSpace(ModificarNom) &&
                !string.IsNullOrWhiteSpace(ModificarCiutat) &&
                !string.IsNullOrWhiteSpace(ModificarCamp) &&
                !string.IsNullOrWhiteSpace(ModificarFoto) &&
                !string.IsNullOrWhiteSpace(ModificarCategoria))
                esValid = true;
            return esValid;
        }

        private void ExecuteLListarEquipCommand(object obj)
        {
            ActualitzarEquips();
        }

        private async void ActualitzarEquips()
        {
            Equips = await databaseContext.RetornarEquips();
        }

    }
}
