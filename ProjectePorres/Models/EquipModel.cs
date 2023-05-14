using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    public class EquipModel
    {
        public string Nom { get; set; }

        public string Ciutat { get; set; }

        public string Camp { get; set; }

        public string Foto { get; set; }

        public string Categoria { get; set; }

        public EquipModel() { }

        public EquipModel(string nom, string ciutat, string camp, string foto, string categoria)
        {
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
