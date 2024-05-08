using IniParser;
using IniParser.Model;
using System.IO;
using System.Windows.Markup;

namespace ProjectePorres.Data
{
    // Simple classe per treballar amb el fitxer de configuració de manera fàcil i globalment.
    public class ConfigContext
    {
        // Atributs
        private readonly FileIniDataParser parser;
        
        // Propietats
        public IniData? Config { get; set; }
        public string? RutaArxiu { get; set; }

        // Constructor
        public ConfigContext(string rutaArxiu)
        {
            RutaArxiu = rutaArxiu;
            parser = new();
            if (File.Exists(RutaArxiu))
                Config = parser.ReadFile(RutaArxiu);
            else
            {
                CrearArxiuPerDefecte();
            }
        }

        // Mètode per utilitat, ja es llegeix l'arxiu per defecte.
        public void LlegirConfiguracio() => Config = parser.ReadFile(RutaArxiu);

        // Mètode per escriure dades al fitxer de configuració.
        public void EscriureConfiguracio(string clau, string camp, string nouValor)
        {
            Config![clau][camp] = nouValor;
            parser.WriteFile(RutaArxiu, Config);
        }

        // Retorna el valor que hagis especificat.
        public string RetornarValor(string clau, string camp)
        {
            return Config![clau][camp];
        }

        private void CrearArxiuPerDefecte()
        {
            // Crea l'arxiu si no existeix.
            var arxiu = File.Create(RutaArxiu!);
            arxiu.Close();

            // Creem un objecte on guardarem les dades.
            IniData defaultData = parser.ReadFile(RutaArxiu);
            
            // UserSessionId
            defaultData["UserSessionId"]["Id"] = "0";
            defaultData["CurrentId"]["Id"] = "0";

            // App Theme
            defaultData["AppTheme"]["PrimaryColor"] = "";
            defaultData["AppTheme"]["SecondaryColor"] = "";
            defaultData["AppTheme"]["PrimaryIndex"] = "0";
            defaultData["AppTheme"]["SecondaryIndex"] = "0";
            defaultData["AppTheme"]["DarkMode"] = "False";

            // Escrivim les dades al fitxer amb els valors per defecte.
            parser.WriteFile(RutaArxiu, defaultData);

            // El primer cop haurem de llegir la configuració.
            LlegirConfiguracio();
        }
    }
}
