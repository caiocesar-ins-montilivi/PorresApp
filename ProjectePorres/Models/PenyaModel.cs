using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Models
{
    public class PenyaModel
    {
        public int IdPenya { get; set; }
        
        public string Nom { get; set; }

        public PenyaModel(int idPenya, string nom)
        {
            IdPenya = idPenya;
            Nom = nom;
        }
    }
}
