using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProjectePorres.Model
{
    public class UsuariModel
    {
        public int Id { get; set; }

        public string Dni { get; set; }

        public string Nom { get; set; }

        public string Cognom { get; set; }

        public string Correu { get; set; }

        public int Puntuacio { get; set; }

        public string Contrasenya { get; set; }

        public bool EsAdmin { get; set; }

        public UsuariModel() { }

        public UsuariModel(int id, string dni, string nom, string cognom, int puntuacio, bool esAdmin)
        {
            Id = id;
            Dni = dni;
            Nom = nom;
            Cognom = cognom;
            Puntuacio = puntuacio;
            EsAdmin = esAdmin;
        }

        public override string ToString()
        {
            return $"{Id}, " +
                   $"{Dni}, " +
                   $"{Nom}, " +
                   $"{Cognom}, " +
                   $"{Puntuacio}, " +
                   $"{EsAdmin}";
        }
    }
}
