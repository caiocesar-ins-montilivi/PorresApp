using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace ProjectePorres.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Server=(localhost); Database=PorraGirona; Integrated Security=true";
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
