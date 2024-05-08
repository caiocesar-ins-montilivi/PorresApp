using ProjectePorres.Models;
using System.Collections.ObjectModel;

namespace ProjectePorres.Model
{
    public class EquipModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Ciutat { get; set; }

        public string Camp { get; set; }

        public string Foto { get; set; }

        public string Categoria { get; set; }

        public ObservableCollection<JugadorModel> Jugadors { get; set; }

        public EquipModel() { }

        public EquipModel(int id, string nom, string ciutat, string camp, string foto, string categoria)
        {
            Id = id;
            Nom = nom;
            Ciutat = ciutat;
            Camp = camp;
            Foto = foto;
            Categoria = categoria;
        }

        public override string ToString()
        {
            return $"{Nom}, " +
                   $"{Ciutat}, " +
                   $"{Camp}, " +
                   $"{Categoria}, " +
                   $"{Foto}";
        }
    }
}
