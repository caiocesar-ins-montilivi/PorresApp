using ProjectePorres.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Models
{
    class PartitsModel
    {
        public ObservableCollection<PartitModel> ColeccioPartits { get; set; }

        public PartitsModel()
        {
            // Per proves

            EquipModel barca = new("Barça", "Barcelona", "Spotify Camp Nou", "", "Primera divisió");
            EquipModel girona = new("Girona", "Girona", "Camp Montilivi", "", "Primera divisió");

            ColeccioPartits = new ObservableCollection<PartitModel>()
            {
                new PartitModel(barca, girona, new DateTime(2023, 22, 05), "2023", 0, 0, false),
                new PartitModel(girona, barca, new DateTime(2023, 22, 05), "2023", 0, 0, false),
                new PartitModel(girona, barca, new DateTime(2023, 22, 05), "2023", 0, 0, false),
            };

        }
    }
}
