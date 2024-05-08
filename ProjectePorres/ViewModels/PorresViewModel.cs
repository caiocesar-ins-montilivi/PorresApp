using ProjectePorres.Data;
using ProjectePorres.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectePorres.ViewModels
{
    class PorresViewModel : BaseViewModel
    {
        // Camps
        private DatabaseContext databaseContext;
        private ObservableCollection<PartitModel>? _partits;

        // Propietats
        public ObservableCollection<PartitModel> Partits
        {
            get => _partits;
            set
            {
                _partits = value;
                OnPropertyChanged(nameof(PartitModel));
            }
        }

        // Commands
        public ICommand AfegirPartitCommand { get; }

        // Constructor
        public PorresViewModel()
        {
            string connectionString = "Server=localhost; Database=PorraGirona; Uid=root; Pwd=;";
            databaseContext = new DatabaseContext(connectionString);
            Partits = new ObservableCollection<PartitModel>() { };

            AfegirPartitCommand = new CommandViewModel(ExecuteAfegirPartitCommand, CanExecuteAfegirPartitCommand);
        }

        private void ExecuteAfegirPartitCommand(object obj)
        {

        }

        private bool CanExecuteAfegirPartitCommand(object obj)
        {
            bool esValid = false;
            return esValid;
        }
    }
}
