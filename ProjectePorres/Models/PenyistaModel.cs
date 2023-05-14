using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    public class PenyistaModel : UsuariModel
    {
        public PenyistaModel(int id, string dni, string nom, string cognom, int puntuacio, bool esAdmin) : base(id, dni, nom, cognom, puntuacio, esAdmin) { }
    }
}
