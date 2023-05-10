using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Model
{
    class Partit
    {
        public Equip EquipLocal { get; set; }

        public Equip EquipVisitant { get; set; }

        public DateTime DataHoraPartit { get; set; }

        public string Temporada { get; set; }

        public int GolsLocal { get; set; }

        public int GolsVisitant { get; set; }

        public bool ResultatsComplerts { get; set; }

        public Partit(Equip equipLocal, Equip equipVisitant, DateTime dataHoraPartit, string temporada, int golsLocal, int golsVisitant, bool resultatsComplerts)
        {
            EquipLocal = equipLocal;
            EquipVisitant = equipVisitant;
            DataHoraPartit = dataHoraPartit;
            Temporada = temporada;
            GolsLocal = golsLocal;
            GolsVisitant = golsVisitant;
            ResultatsComplerts = resultatsComplerts;
        }

        public override string ToString()
        {
            return $"{EquipLocal.Nom} - [{GolsLocal}] VS {EquipVisitant.Nom} - [{GolsVisitant}]\n" +
                   $"Hora partit: {DataHoraPartit}\n" +
                   $"Temporada: {Temporada}\n" +
                   $"Partit acabat {ResultatsComplerts}";
        }
    }
}
