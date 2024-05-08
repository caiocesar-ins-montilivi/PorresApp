using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Models
{
    public class PuntuacionsModel
    {
        public int Id { get; set; }

        public int IdPenyista { get; set; }

        public string Alias { get; set; }

        public int Puntuacio { get; set; }

        public string Temporada { get; set; }

        public PuntuacionsModel(int id, int idPenyista, string alias, int puntuacio, string temporada)
        {
            Id = id;
            IdPenyista = idPenyista;
            Alias = alias;
            Puntuacio = puntuacio;
            Temporada = temporada;
        }
    }
}
