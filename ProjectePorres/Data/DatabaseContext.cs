using MySql.Data.MySqlClient;
using ProjectePorres.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectePorres.Data
{
    public class DatabaseContext
    {
        private readonly string connectionString;

        public DatabaseContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Comprova que tenim connexió a la base de dades de manera asíncrona.
        public async Task<bool> ComprovarConnexio()
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        // Mètode encarregat de validar un usuari de manera asíncrona.
        public async Task<bool> ValidarUsuari(string nomUsuari, string contrasenya)
        {
            using var connection = new MySqlConnection(connectionString);
            string hashGuardat = string.Empty; // Hash de la contrasenya guardada a la base de dades.
            bool esValid = false;

            try
            {
                await connection.OpenAsync();

                const string query = "SELECT Contrasenya FROM Usuaris WHERE NomUsuari = @nom_usuari;";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@nom_usuari", nomUsuari);

                var reader = await command.ExecuteReaderAsync();

                // Afagem el hash de la base de dades.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var passwordHash = reader[0]; // Afaga el hash.
                        hashGuardat = passwordHash.ToString()!; // El guarda com a un string.
                    }

                    // Convertim la contrasenya de l'usuari en un hash.
                    using SHA256 sha256 = SHA256.Create();
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(contrasenya);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    // Ara comparem el hash de l'usuari amb el hash guardat.
                    if (hashGuardat == hash) esValid = true; // Si coincideixen llavors l'usuari és correcte.
                    else esValid = false; // No coincideix. l'Usuari no es correcte.
                }
                else esValid = false; // ERROR: l'Usuari no existeix.
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al connectar a la base de dades: {ex.Message}");
                esValid = false;
            }

            return esValid;
        }

        // Mètode encarregat de registrar un usuari de manera asíncrona.
        public async Task<bool> RegistrarUsuari(string nomUsuari, string dni, string nom, string cognom, string correu, string password)
        {
            bool registreCorrecte;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                const string query = "INSERT INTO Usuaris(NomUsuari, Dni, Nom, Cognom, Correu, Contrasenya, Puntuacio, EsAdmin) " +
                    "VALUES(@NomUsuari, @Dni, @Nom, @Cognom, @Correu, SHA2(@Contrasenya, 256), 0, False)";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@NomUsuari", nomUsuari);
                command.Parameters.AddWithValue("@Dni", dni);
                command.Parameters.AddWithValue("@Nom", nom);
                command.Parameters.AddWithValue("@Cognom", cognom);
                command.Parameters.AddWithValue("@Correu", correu);
                command.Parameters.AddWithValue("@Contrasenya", password);
                await command.ExecuteNonQueryAsync();
                registreCorrecte = true;
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Número d'error específic per entrada duplicada.
            {
                Trace.WriteLine($"Error entrada duplicada: {ex.Message}");
                registreCorrecte = false;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al connectar a la base de dades: {ex.Message}");
                registreCorrecte = false;
            }

            return registreCorrecte;
        }

        // Retorna un model d'usuari segons el seu nom d'usuari de manera asíncrona.
        public async Task<UsuariModel> RetornarUsuariPerNom(string nomUsuari)
        {
            UsuariModel? usuariModel = null;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                const string query = "SELECT * FROM Usuaris WHERE NomUsuari = @NomUsuari";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@NomUsuari", nomUsuari);

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _id = reader[0];
                        var _dni = reader[2];
                        var _nom = reader[3];
                        var _cognom = reader[4];
                        var _correu = reader[5];
                        var _puntuacio = reader[7];
                        var _esAdmin = reader[8];

                        usuariModel = new(
                            Convert.ToInt32(_id),
                            _dni.ToString(),
                            _nom.ToString(),
                            _cognom.ToString(),
                            Convert.ToInt32(_puntuacio),
                            Convert.ToBoolean(_esAdmin));
                    }
                }
                else Trace.WriteLine($"No hi ha cap usuari anomenat {nomUsuari}");
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al connectar a la base de dades: {ex.Message}");
            }

            return usuariModel;
        }

        // Retorna un model d'usuari segons el seu nom d'usuari de manera asíncrona.
        public async Task<UsuariModel> RetornarUsuariPerId(int idUsuari)
        {
            UsuariModel? usuariModel = null;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                const string query = "SELECT * FROM Usuaris WHERE Id = @idUsuari";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@idUsuari", idUsuari);

                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _id = reader[0];
                        var _dni = reader[2];
                        var _nom = reader[3];
                        var _cognom = reader[4];
                        var _correu = reader[5];
                        var _puntuacio = reader[7];
                        var _esAdmin = reader[8];

                        usuariModel = new(
                            Convert.ToInt32(_id),
                            _dni.ToString(),
                            _nom.ToString(),
                            _cognom.ToString(),
                            Convert.ToInt32(_puntuacio),
                            Convert.ToBoolean(_esAdmin));
                    }
                }
                else Trace.WriteLine($"No hi ha cap usuari amb la id: {idUsuari}");
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al connectar a la base de dades: {ex.Message}");
            }

            return usuariModel;
        }

        // Mètode encarregat de registrar un partit de manera asíncrona.
        public async Task<bool> RegistrarPartit(PartitModel partit)
        {
            bool registreCorrecte;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                // string query = "INSERT INTO Partits()";

                registreCorrecte = true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al registrar un partit: {ex.Message}");
                registreCorrecte = false;
            }

            return registreCorrecte;
        }

        // Retorna els partits de manera asíncrona.
        public async Task<ObservableCollection<PartitModel>> RetornarPartits()
        {
            ObservableCollection<PartitModel> coleccioPartits;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                // string query = "SELECT * FROM Partits";
                
                coleccioPartits = new ObservableCollection<PartitModel>();
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al retornar partits: {ex.Message}");
                coleccioPartits = new ObservableCollection<PartitModel>();
            }

            return coleccioPartits;
        }

        // Mètode encarregat de registrar un partit de manera asíncrona.
        public async Task<bool> RegistrarEquip(string nom, string imatge, string ciutat, string camp, string categoria)
        {
            bool registreCorrecte;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                // string query = "INSERT INTO Equips()";

                registreCorrecte = true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al registrar un equip: {ex.Message}");
                registreCorrecte = false;
            }

            return registreCorrecte;
        }

        // Retorna els equips de manera asíncrona.
        public async Task<ObservableCollection<EquipModel>> RetornarEquips()
        {
            ObservableCollection<EquipModel> coleccioPartits = new ObservableCollection<EquipModel>();

            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Equips";

                MySqlCommand command = new(query, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _idEquip = reader[0];
                        var _nom = reader[1];
                        var _imatge = reader[2];
                        var _ciutat = reader[3];
                        var _camp = reader[4];
                        var _categoria = reader[5];

                        EquipModel equip = new(
                            Convert.ToInt32(_idEquip),
                            _nom.ToString(),
                            _ciutat.ToString(),
                            _camp.ToString(),
                            _imatge.ToString(),
                            _categoria.ToString()
                        );
                        coleccioPartits.Add(equip);
                    }
                }
                else Trace.WriteLine($"No hi ha cap equip registrat.");
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al retornar equips: {ex.Message}");
            }

            return coleccioPartits;
        }
    }
}
