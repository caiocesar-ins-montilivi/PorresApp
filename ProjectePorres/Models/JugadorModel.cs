using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Models
{
    public class JugadorModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Temporada { get; set; }

        public int Dorsal { get; set; }

        public JugadorModel(int id, string nom, string temporada, int dorsal)
        {
            Id = id;
            Nom = nom;
            Temporada = temporada;
            Dorsal = dorsal;
        }

        public override string ToString()
        {
            return $"{Id}, " +
                   $"{Nom}, " +
                   $"{Temporada}, " +
                   $"{Dorsal}";
        }
    }
}
