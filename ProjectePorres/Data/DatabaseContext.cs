﻿using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System;
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

        public bool RegistrarUsuari(string nomUsuari, string dni, string nom, string cognom, string correu, string password)
        {
            bool registreCorrecte;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                const string query = "INSERT INTO Usuaris(NomUsuari, Dni, Nom, Cognom, Correu, Contrasenya, Puntuacio, EsAdmin) " +
                    "VALUES(@NomUsuari, @Dni, @Nom, @Cognom, @Correu, SHA2(@Contrasenya, 256), 0, False)";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@NomUsuari", nomUsuari);
                command.Parameters.AddWithValue("@Dni", dni);
                command.Parameters.AddWithValue("@Nom", nom);
                command.Parameters.AddWithValue("@Cognom", cognom);
                command.Parameters.AddWithValue("@Correu", correu);
                command.Parameters.AddWithValue("@Contrasenya", password);
                command.ExecuteNonQueryAsync();
                registreCorrecte = true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine($"Error al connectar a la base de dades: {ex.Message}");
                registreCorrecte = false;
            }

            return registreCorrecte;
        }

        public UsuariModel RetornarUsuariPerNom(string nomUsuari)
        {
            UsuariModel? usuariModel = null;
            using var connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                const string query = "SELECT * FROM Usuaris WHERE NomUsuari = @NomUsuari";
                MySqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@NomUsuari", nomUsuari);

                var reader = command.ExecuteReader();
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
    }
}