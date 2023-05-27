using System;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using ProjectePorres.Model;

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
        public bool ValidarUsuari(string nomUsuari, string contrasenya)
        {
            using var connection = new MySqlConnection(connectionString);
            string hashGuardat = string.Empty; // Hash de la contrasenya guardada a la base de dades.
            bool esValid = false;

            try
            {
                connection.Open();

                const string query = "SELECT Contrasenya FROM Usuaris WHERE NomUsuari = @nom_usuari;";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@nom_usuari", nomUsuari);

                var reader = command.ExecuteReader();

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

        public async Task<bool> ComprovarSessionsActives()
        {
            bool sessioActiva = false;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE SesiónActiva = 1";
                MySqlCommand command = new(query, connection);
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                if (count > 0) 
                    sessioActiva = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error durant la verificació de la sesión activa: {ex.Message}");
            }

            return sessioActiva;
        }

        public async void ActualitzarSessioActiva(int idUsuari, bool canviar)
        {
            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
                string query;
                if (canviar)
                {
                    query = "INSERT INTO Sessions(idUsuari) VALUES(@idUsuari)";
                    MySqlCommand command = new(query, connection);
                    command.Parameters.AddWithValue("@idUsuari", idUsuari);
                    await command.ExecuteNonQueryAsync();
                }
                else
                {
                    query = "TRUNCATE Sessions";
                    MySqlCommand command = new(query, connection);
                    await command.ExecuteNonQueryAsync();
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error durant l'actualització de la sesión activa: {ex.Message}");
            }
        }
    }
}
