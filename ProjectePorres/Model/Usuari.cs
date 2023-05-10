using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    class Usuari
    {
        public string Dni { get; set; }

        public string Nom { get; set; }

        public string Cognom { get; set; }

        public int Puntuacio { get; set; }

        public bool EsAdmin { get; set; }

        public Usuari() { }

        public Usuari(string dni, string nom, string cognom, int puntuacio, bool esAdmin)
        {
            Dni = dni;
            Nom = nom;
            Cognom = cognom;
            Puntuacio = puntuacio;
            EsAdmin = esAdmin;
        }

        public override string ToString()
        {
            return $"{Dni}, " +
                   $"{Nom}, " +
                   $"{Cognom}, " +
                   $"{Puntuacio}, " +
                   $"{EsAdmin}";
        }
    }
}
