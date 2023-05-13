using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    class Administrador : Usuari
    {
        public Administrador(string dni, string nom, string cognom, int puntuacio, bool esAdmin) : base(dni, nom, cognom, puntuacio, esAdmin) { }
        
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
