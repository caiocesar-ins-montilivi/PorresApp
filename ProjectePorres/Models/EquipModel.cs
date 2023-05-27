using ProjectePorres.Models;
using System.Collections.ObjectModel;

namespace ProjectePorres.Model
{
    public class EquipModel
    {
        public string Nom { get; set; }

        public string Ciutat { get; set; }

        public string Camp { get; set; }

        public string Foto { get; set; }

        public string Categoria { get; set; }

        public ObservableCollection<JugadorModel> Jugadors { get; set; }

        public EquipModel() { Jugadors = new ObservableCollection<JugadorModel>(); }

        public EquipModel(string nom, string ciutat, string camp, string foto, string categoria, ObservableCollection<JugadorModel> jugadors)
        {
            Nom = nom;
            Ciutat = ciutat;
            Camp = camp;
            Foto = foto;
            Categoria = categoria;
            Jugadors = new ObservableCollection<JugadorModel>(jugadors);
        }

        private string MostrarJugadors()
        {
            string jugador = string.Empty;
            foreach (JugadorModel j in Jugadors)
                jugador += j.ToString();
            return jugador;
        }

        public override string ToString()
        {
            return $"{Nom}, " +
                   $"{Ciutat}, " +
                   $"{Camp}, " +
                   $"{Categoria}, " +
                   $"{Foto}" +
                   $"{MostrarJugadors()}";
        }
    }
}
