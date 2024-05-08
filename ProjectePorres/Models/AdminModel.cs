using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    public class AdminModel : UsuariModel
    {
        public AdminModel(int id, string dni, string nom, string cognom, int puntuacio, bool esAdmin) : base(id, dni, nom, cognom, puntuacio, esAdmin) { }

        public bool EliminarUsuari()
        {
            return true;
        }

        public bool GestionarResultats()
        {
            return true;
        }
    }
}
