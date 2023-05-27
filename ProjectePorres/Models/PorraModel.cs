using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Models
{
    public class PorraModel
    {
        public int Id { get; set; }

        public int GolsLocal { get; set; }

        public int GolsVisitant { get; set; }

        public DateTime Data { get; set; }

        public int IdGolejadorsLocal { get; set; }

        public int IdGolejadorsVisitant { get; set; }

        public int IdPenyista { get; set; }

        public int IdPartit { get; set; }

        public PorraModel(int id, int golsLocal, int golsVisitant, DateTime data, int idGolejadorsLocal, int idGolejadorsVisitant, int idPenyista, int idPartit)
        {
            Id = id;
            GolsLocal = golsLocal;
            GolsVisitant = golsVisitant;
            Data = data;
            IdGolejadorsLocal = idGolejadorsLocal;
            IdGolejadorsVisitant = idGolejadorsVisitant;
            IdPenyista = idPenyista;
            IdPartit = idPartit;
        }

    }
}
